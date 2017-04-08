using HD.Core;
using HD.Domain.Models;
using HD.IdentityManager;
using HD.Service.Interface;
using HD.Site.Areas.Admin.Models.ViewModel;
using HD.Site.Common;
using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HD.Site.Areas.Admin.Controllers
{
    [Authorize]
    public class TypeCategoryController : BaseController
    {
        private readonly ITypeCategoryService _typeCatSrv;

        public TypeCategoryController()
        {
            _typeCatSrv = IoC.Resolve<ITypeCategoryService>();
        }

        // GET: Admin/TypeCategory
        public ActionResult Index(TypeCategoryIndexModel model, int? page)
        {
            var currentPage = page.HasValue ? page.Value - 1 : 0;
            var pageSize = 10;
            int total = 0;
            var lst = _typeCatSrv.GetByfilter(model.KeyWord, currentPage, pageSize, out total);
            model.TypeCategories = new PagedList<TypeCategory>(lst, currentPage, pageSize, total);
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateNew()
        {
            var model = new TypeCategory();
            model.Status = true;
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateNew(TypeCategory model)
        {
            try
            {
                model.URL = "/" + StringUtil.UnsignToString(model.Name);
                model.CreateBy = CurrentInstance.Instance.CurrentUser.UserName;
                model.CreateDate = DateTime.Now;
                _typeCatSrv.CreateNew(model);
                _typeCatSrv.CommitChange();
                AlertSuccess(InfoString.CREATE_SUCCESSFULL);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogError(ex);
                AlertError(InfoString.ERROR_SYSTEM);
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var model = _typeCatSrv.GetBykey(id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Update(TypeCategory model)
        {
            try
            {
                model.URL = "/" + StringUtil.UnsignToString(model.Name);
                model.UpdateBy = CurrentInstance.Instance.CurrentUser.UserName;
                model.UpdateDate = DateTime.Now;
                _typeCatSrv.Update(model);
                _typeCatSrv.CommitChange();
                AlertSuccess(InfoString.UPDATE_SUCCESSFULL);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogError(ex);
                AlertError(InfoString.ERROR_SYSTEM);
                return View(model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var flag = _typeCatSrv.CheckContainProductCategory(id);
                if (flag)
                {
                    AlertWarning("Tồn tại danh mục thuộc nhóm này, xóa thất bại, hãy thử lại sau.");
                }
                else
                {
                    _typeCatSrv.Delete(id);
                    _typeCatSrv.CommitChange();
                    AlertSuccess(InfoString.DELETE_SUCCESSFULL);
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
                AlertError(InfoString.ERROR_SYSTEM);
            }

            return RedirectToAction("Index");
        }
    }
}