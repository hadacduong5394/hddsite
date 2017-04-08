using BotDetect.Web.Mvc;
using HD.Context;
using HD.Core;
using HD.IdentityManager;
using HD.Service.Interface;
using HD.Site.Areas.Admin.Models;
using HD.Site.Common;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HD.Site.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController()
        {
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var model = new LoginViewModel();
            MvcCaptcha loginCaptCha = new MvcCaptcha("LoginCaptCha");
            loginCaptCha.Reset();
            loginCaptCha.UserInputID = "CaptchaCode";
            loginCaptCha.AutoClearInput = true;
            loginCaptCha.AutoUppercaseInput = true;
            model.LoginCaptCha = loginCaptCha;
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CaptchaValidation("CaptchaCode", "LoginCaptCha", "Mã xác nhận không đúng.")]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = UserManager.Instance.UserManagerment.Find(model.UserName, model.Password);
                    if (user != null)
                    {
                        if (!user.Status)
                        {
                            ViewData["ErrorLogin"] = "Tài khoản này đang bị khóa.";
                        }
                        else if (user.Group != 1)
                        {
                            ViewData["ErrorLogin"] = "Bạn không thuộc nhóm quản trị, hãy quay lại.";
                        }
                        else
                        {
                            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                            ClaimsIdentity identity = UserManager.Instance.UserManagerment.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                            AuthenticationProperties props = new AuthenticationProperties();
                            authenticationManager.SignIn(props, identity);
                            return RedirectToLocal(returnUrl);
                        }
                    }
                    else
                    {
                        ViewData["ErrorLogin"] = "Sai tên đăng nhập hoặc mật khẩu.";
                    }
                }
                MvcCaptcha loginCaptCha = new MvcCaptcha("LoginCaptCha");
                loginCaptCha.UserInputID = "CaptchaCode";
                model.LoginCaptCha = loginCaptCha;
                loginCaptCha.AutoClearInput = true;
                loginCaptCha.AutoUppercaseInput = true;
                ViewBag.ReturnUrl = returnUrl;
                return View(model);
            }
            catch (Exception ex)
            {
                LogError(ex);
                MvcCaptcha loginCaptCha = new MvcCaptcha("LoginCaptCha");
                loginCaptCha.UserInputID = "CaptchaCode";
                model.LoginCaptCha = loginCaptCha;
                loginCaptCha.AutoClearInput = true;
                loginCaptCha.AutoUppercaseInput = true;
                ViewBag.ReturnUrl = returnUrl;
                ViewData["ErrorLogin"] = "Lỗi hệ thống.";
                return View(model);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult LostPassword()
        {
            var model = new LostPasswordViewModel();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> LostPassword(LostPasswordViewModel model)
        {
            try
            {
                var user = await UserManager.Instance.UserManagerment.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    AlertInfo("Người dùng này không tồn tại.");
                    return View(model);
                }
                else
                {
                    if (!user.Email.Equals(model.Email))
                    {
                        AlertWarning("Email này không đúng với email đăng ký tài khoản, hãy thử lại.");
                        return View(model);
                    }
                    else
                    {
                        var newPassword = RandomPassword.GetARandomPassword();
                        var newPassHash = UserManager.Instance.UserManagerment.PasswordHasher.HashPassword(newPassword);
                        user.PasswordHash = newPassHash;
                        var result = await UserManager.Instance.UserManagerment.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            //send Email
                            IoC.Resolve<IEmailService>().SendEmail(model.Email, "Mật khẩu đã được đặt lại thành công.", "Mật khẩu mới: " + newPassword);
                            AlertSuccess("Mật khẩu đã được đặt lại, hãy truy cập email của bạn để lấy lại mật khẩu.");
                            return RedirectToAction("Login");
                        }
                        AlertError(InfoString.ERROR_SYSTEM);
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
                AlertError(InfoString.ERROR_SYSTEM);
                return View(model);
            }
        }
    }
}