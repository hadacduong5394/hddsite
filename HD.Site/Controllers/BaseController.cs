using HD.Core;
using HD.Domain.Models;
using HD.Service.Interface;
using System;
using System.Web.Mvc;

namespace HD.Site.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
        }

        protected void LogError(Exception ex)
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