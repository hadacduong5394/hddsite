using HD.Core;
using HD.Domain.Models;
using HD.IdentityManager;
using HD.Service.Interface;
using HD.Site.Areas.Admin.Models.ViewModel;
using HD.Site.Common;
using MvcPaging;
using System;
using System.Web.Mvc;

namespace HD.Site.Areas.Admin.Controllers
{
    [Authorize]
    public class ArticleCategoryController : BaseController
    {
        public ArticleCategoryController()
        {
        }

        // GET: Admin/ArticleCategory
        public ActionResult Index(ArticleCatIndexModel model, int? page)
        {
            var artCatSrv = IoC.Resolve<IArticleCategoryService>();
            var currentPage = page.HasValue ? page.Value - 1 : 0;
            var pageSize = 10;
            int total = 0;
            var lst = artCatSrv.GetByFilter(model.CatId, model.Name, currentPage, pageSize, out total);
            model.Parents = artCatSrv.GetParents();
            model.ArticleCategories = new PagedList<ArcticleCategory>(lst, currentPage, pageSize, total);
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateNew(int? parentId)
        {
            var artCatSrv = IoC.Resolve<IArticleCategoryService>();
            var model = new ArcticleCategory();
            model.ParentId = parentId;
            ViewData["Parents"] = artCatSrv.GetParents();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateNew(ArcticleCategory model)
        {
            var artCatSrv = IoC.Resolve<IArticleCategoryService>();
            try
            {
                model.CreateBy = CurrentInstance.Instance.CurrentUser.UserName;
                model.CreateDate = DateTime.Now;
                model.URL = "/" + UnsignToString(model.CatName);
                artCatSrv.CreateNew(model);
                artCatSrv.CommitChange();
                AlertSuccess(InfoString.CREATE_SUCCESSFULL);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogError(ex);
                AlertError(InfoString.ERROR_SYSTEM);
                ViewData["Parents"] = artCatSrv.GetParents();
                return View(model);
            }
            
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var artCatSrv = IoC.Resolve<IArticleCategoryService>();
            var model = artCatSrv.GetByKey(id);
            ViewData["Parents"] = artCatSrv.GetParents();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Update(ArcticleCategory model)
        {
            var artCatSrv = IoC.Resolve<IArticleCategoryService>();
            try
            {
                model.UpdateBy = CurrentInstance.Instance.CurrentUser.UserName;
                model.UpdateDate = DateTime.Now;
                model.URL = "/" + UnsignToString(model.CatName);
                artCatSrv.Update(model);
                artCatSrv.CommitChange();
                AlertSuccess(InfoString.UPDATE_SUCCESSFULL);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogError(ex);
                AlertError(InfoString.ERROR_SYSTEM);
                ViewData["Parents"] = artCatSrv.GetParents();
                return View(model);
            }            
        }

        [HttpPost]
        public JsonResult ViewDetail(int id)
        {
            var artCatSrv = IoC.Resolve<IArticleCategoryService>();
            var m = artCatSrv.GetByKey(id);

            return Json(new
            {
                Name = m.CatName,
                URL = m.URL,
                Parent = m.ParentId.HasValue ? artCatSrv.GetByKey(m.ParentId.Value).CatName : string.Empty,
                CreateBy = m.CreateBy,
                CreateDate = m.CreateDate.Value.ToString("dd-MM-yyyy"),
                UpdateBy = m.UpdateBy,
                UpdateDate = m.UpdateDate.HasValue ? m.UpdateDate.Value.ToString("dd-MM-yyyy") : string.Empty
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var artCatSrv = IoC.Resolve<IArticleCategoryService>();
                if (artCatSrv.ExistChilds(id))
                {
                    AlertError("Danh mục này đang tồn tại các danh mục con, hãy thử lại sau.");
                    return RedirectToAction("Index");
                }

                artCatSrv.Delete(id);
                artCatSrv.CommitChange();
                AlertSuccess(InfoString.DELETE_SUCCESSFULL);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogError(ex);
                AlertError(InfoString.ERROR_SYSTEM);
                return RedirectToAction("Index");
            }
        }
    }
}