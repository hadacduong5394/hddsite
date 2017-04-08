using HD.Context;
using HD.Core;
using HD.IdentityManager;
using HD.IdentityManager.IService;
using HD.Site.Areas.Admin.Models;
using HD.Site.Common;
using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HD.Site.Areas.Admin.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IGroupService _groupService;

        public UserController()
        {
            _groupService = IoC.Resolve<IGroupService>();
        }

        // GET: User
        public ActionResult Index(UserViewModel model, int? page)
        {
            var currentPage = page.HasValue ? page.Value - 1 : 0;
            var pageSize = 10;
            var total = 0;
            var lst = GetByFilter(model.KeyWord, currentPage, pageSize, out total);
            model.Users = new PagedList<ApplicationUser>(lst, currentPage, pageSize, total);
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateNew()
        {
            var groups = _groupService.GetAll();
            var lstGroupDetail = new List<GroupVM>();
            var user = new ApplicationUser();
            user.Id = Guid.NewGuid().ToString();
            user.Group = 1;
            foreach (var item in groups)
            {
                lstGroupDetail.Add(new GroupVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    IsCheck = false,
                    Descreption = item.Descreption
                });
            }
            var model = new UserModelDetail();
            model.User = user;
            model.Groups = lstGroupDetail;
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateNew(UserModelDetail model)
        {
            if (!model.Groups.Any(n => n.IsCheck == true))
            {
                AlertWarning("Người dùng này chưa thuộc nhóm người dùng nào cả, hãy chọn nhóm của người dùng.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var checkUser = await UserManager.Instance.UserManagerment.FindByNameAsync(model.User.UserName);
                    if (checkUser != null)
                    {
                        AlertWarning(InfoString.Instance.SetContainString("Tên đăng nhập"));
                        return View(model);
                    }

                    var userByEmail = await UserManager.Instance.UserManagerment.FindByEmailAsync(model.User.Email);
                    if (userByEmail != null)
                    {
                        AlertWarning(InfoString.Instance.SetContainString("Email"));
                        return View(model);
                    }
                    model.User.CreateBy = CurrentInstance.Instance.CurrentUser.UserName;
                    model.User.CreateDate = DateTime.Now;
                    model.User.BirthDay = DateTime.Now;
                    model.User.Group = 1;
                    var result = await UserManager.Instance.UserManagerment.CreateAsync(model.User, model.User.PasswordHash);
                    if (result.Succeeded)
                    {
                        //add user to group
                        foreach (var item in model.Groups)
                        {
                            if (item.IsCheck)
                            {
                                _groupService.AddUserToGroup(model.User.Id, item.Id);
                            }
                        }
                        _groupService.CommitChanges();
                        AlertSuccess(InfoString.CREATE_SUCCESSFULL);
                    }
                    else
                    {
                        AlertWarning(InfoString.INVALID_INFO);
                        return View(model);
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex);
                    AlertError(InfoString.ERROR_SYSTEM);
                }
                return RedirectToAction("Index");
            }
            AlertWarning(InfoString.INVALID_INFO);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Update(string id)
        {
            var groups = _groupService.GetAll();
            var lstGroupDetail = new List<GroupVM>();
            var user = await UserManager.Instance.UserManagerment.FindByIdAsync(id);
            var groupsOfUser = _groupService.GetGroupIdsByUserId(user.Id);
            foreach (var item in groups)
            {
                var g = new GroupVM();
                g.Id = item.Id;
                g.Name = item.Name;
                g.IsCheck = false;
                g.Descreption = item.Descreption;
                if (groupsOfUser.Any(n => n.GroupId == item.Id))
                {
                    g.IsCheck = true;
                }

                lstGroupDetail.Add(g);
            }
            var model = new UserModelDetail();
            model.User = user;
            model.Groups = lstGroupDetail;
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UserModelDetail model)
        {
            if (!model.Groups.Any(n => n.IsCheck == true))
            {
                AlertWarning("Người dùng này chưa thuộc nhóm người dùng nào cả, hãy chọn nhóm của người dùng.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                _groupService.BeginTran();
                try
                {
                    var userFindByEmail = await UserManager.Instance.UserManagerment.FindByEmailAsync(model.User.Email);
                    if (userFindByEmail != null && !userFindByEmail.Id.Equals(model.User.Id))
                    {
                        AlertWarning(InfoString.Instance.SetContainString("Email"));
                        return View(model);
                    }
                    var userNeedUpdate = await UserManager.Instance.UserManagerment.FindByIdAsync(model.User.Id);
                    userNeedUpdate.FullName = model.User.FullName;
                    userNeedUpdate.Email = model.User.Email;
                    userNeedUpdate.BirthDay = model.User.BirthDay;
                    userNeedUpdate.Status = model.User.Status;
                    userNeedUpdate.UpdateBy = CurrentInstance.Instance.CurrentUser.UserName;
                    userNeedUpdate.UpdateDate = DateTime.Now;
                    var result = await UserManager.Instance.UserManagerment.UpdateAsync(userNeedUpdate);
                    if (result.Succeeded)
                    {
                        //delete oder group of user
                        _groupService.DeleteGroupOfUser(model.User.Id);
                        _groupService.CommitChanges();

                        //add user to new groups
                        foreach (var item in model.Groups)
                        {
                            if (item.IsCheck)
                            {
                                _groupService.AddUserToGroup(model.User.Id, item.Id);
                            }
                        }
                        _groupService.CommitChanges();
                        _groupService.CommitTran();
                        AlertSuccess(InfoString.UPDATE_SUCCESSFULL);
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex);
                    AlertError(InfoString.ERROR_SYSTEM);
                    _groupService.RollbackTran();
                }
                return RedirectToAction("Index");
            }
            AlertWarning(InfoString.INVALID_INFO);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id)
        {
            _groupService.BeginTran();
            try
            {
                var user = await UserManager.Instance.UserManagerment.FindByIdAsync(id);
                if (user.UserName.Equals(CurrentInstance.Instance.CurrentUser.UserName))
                {
                    AlertWarning("Tài khoản này đang sử dụng hiện hành, hãy dùng tài khoản có quyền xóa khác để xóa.");
                    return RedirectToAction("Index");
                }

                _groupService.DeleteGroupOfUser(id);
                _groupService.CommitChanges();

                await UserManager.Instance.UserManagerment.DeleteAsync(user);
                _groupService.CommitTran();
                AlertSuccess(InfoString.DELETE_SUCCESSFULL);
            }
            catch (Exception ex)
            {
                LogError(ex);
                AlertError(InfoString.ERROR_SYSTEM);
                _groupService.RollbackTran();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<JsonResult> ViewDetail(string id)
        {
            var currentUser = await UserManager.Instance.UserManagerment.FindByIdAsync(id);

            return Json(new
            {
                userName = currentUser.UserName,
                fullName = currentUser.FullName,
                email = currentUser.Email,
                createDate = currentUser.CreateDate.Value.ToString("dd-MM-yyyy hh:mm:ss"),
                createBy = currentUser.CreateBy,
                updateDate = currentUser.UpdateDate.HasValue ? currentUser.UpdateDate.Value.ToString("dd-MM-yyyy hh:mm:ss") : string.Empty,
                updateBy = currentUser.UpdateBy,
                status = currentUser.Status ? "Kích hoạt" : "Tạm khóa"
            });
        }

        private IList<ApplicationUser> GetByFilter(string keyWord, int currentPage, int pageSize, out int total)
        {
            var query = from a in UserManager.Instance.UserManagerment.Users select a;

            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                query = query.Where(n => n.UserName.Contains(keyWord) || n.Email.Contains(keyWord));
            }
            total = query.Count();
            query = query.OrderBy(n => n.UserName);
            var lst = query.Skip(currentPage * pageSize).Take(pageSize).ToList();
            return lst;
        }

        [HttpGet]
        public ActionResult ChangPassword()
        {
            var model = new ChangePasswordViewModel();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangPassword(ChangePasswordViewModel model)
        {
            try
            {
                var user = await UserManager.Instance.UserManagerment.FindByNameAsync(CurrentInstance.Instance.CurrentUser.UserName);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    var flag = await UserManager.Instance.UserManagerment.FindAsync(user.UserName, model.OlderPassword);
                    if (flag == null)
                    {
                        AlertError("Mật khẩu hiện tại không đúng.");
                        return View(model);
                    }

                    var result = await UserManager.Instance.UserManagerment.ChangePasswordAsync(user.Id, model.OlderPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        AlertSuccess("Thay đổi mật khẩu thành công, đăng nhập lại để tiếp tục phiên làm việc.");
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        AlertWarning(InfoString.INVALID_INFO);
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