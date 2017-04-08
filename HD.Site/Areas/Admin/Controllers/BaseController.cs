using HD.Core;
using HD.Domain.Models;
using HD.Service.Interface;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace HD.Site.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
        }

        protected string UnsignToString(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            input = input.Replace(".", "-");
            input = input.Replace(" ", "-");
            input = input.Replace(",", "-");
            input = input.Replace(";", "-");
            input = input.Replace(":", "-");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2.Replace("--", "-").ToLower();
        }

        protected void LogError(Exception ex)
        {
            try
            {
                var _errorService = IoC.Resolve<IErrorService>();

                Error model = new Error();
                model.Message = ex.Message;
                model.Stacktrace = ex.StackTrace;
                model.CreateDate = DateTime.Now;
                model.Status = false;
                _errorService.CreateNew(model);
                _errorService.CommitChange();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        protected void AlertSuccess(string message)
        {
            TempData["toastrType"] = "success";
            TempData["toastrMessage"] = message;
        }

        protected void AlertInfo(string message)
        {
            TempData["toastrType"] = "info";
            TempData["toastrMessage"] = message;
        }

        protected void AlertWarning(string message)
        {
            TempData["toastrType"] = "warning";
            TempData["toastrMessage"] = message;
        }

        protected void AlertError(string message)
        {
            TempData["toastrType"] = "error";
            TempData["toastrMessage"] = message;
        }

        public void ClearAlert()
        {
            TempData["toastrType"] = null;
            TempData["toastrMessage"] = null;
        }
    }
}