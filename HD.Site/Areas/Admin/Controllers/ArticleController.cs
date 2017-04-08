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
    public class ArticleController : BaseController
    {
        public ArticleController()
        {

        }

        // GET: Admin/Article
        public ActionResult Index(ArticleIndexModel model, int? page)
        {
            var artSrv = IoC.Resolve<IArticleService>();
            var artCatSrv = IoC.Resolve<IArticleCategoryService>();
            var currentPage = page.HasValue ? page.Value - 1 : 0;
            var pageSize = 10;
            int total = 0;
            var lst = artSrv.GetByFilter(model.KeyWord, model.ParentCatId, model.CatId, currentPage, pageSize, out total);
            model.ArtCats = artCatSrv.GetParents();
            model.Cats = artCatSrv.GetChilds(model.ParentCatId);
            model.Articles = new PagedList<Arcticle>(lst, currentPage, pageSize, total);
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateNew(int? catParentId, int? catId)
        {
            var artCatSrv = IoC.Resolve<IArticleCategoryService>();
            var model = new ArticleInputModel();
            var articleModel = new Arcticle();
            model.ArticleModel = articleModel;
            model.ParentCatId = catParentId;
            model.ParentCats = artCatSrv.GetParents();
            model.CatId = catId;
            model.ChildCats = artCatSrv.GetChilds(catParentId);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult CreateNew(Arcticle artModel, int? catParentId, int CatId, string[] tags)
        {
            var artSrv = IoC.Resolve<IArticleService>();
            var artCatSrv = IoC.Resolve<IArticleCategoryService>();
            var tagSrv = IoC.Resolve<ITagService>();
            var lstExisted = new List<Tag>();
            var lstNotExisted = new List<Tag>();
            artCatSrv.BeginTran();
            try
            {
                if (tags.Length > 0)
                {
                    var map = tags.Distinct().ToDictionary(x => StringUtil.UnsignToString(x));

                    foreach (var item in map)
                    {
                        var flag = tagSrv.CheckTitleContain(item.Key);
                        if (!flag)
                        {
                            var tagEntity = new Tag();
                            tagEntity.TagTitle = item.Key;
                            tagEntity.TagName = item.Value;
                            tagEntity.CreateBy = CurrentInstance.Instance.CurrentUser.UserName;
                            tagEntity.CreateDate = DateTime.Now;
                            tagEntity.Status = true;
                            tagSrv.CreateNew(tagEntity);
                            tagSrv.CommitChange();
                            lstNotExisted.Add(tagEntity);
                        }
                        else
                        {
                            lstExisted.Add(tagSrv.GetByKey(item.Key));
                        }
                    }

                    var lstTag = lstExisted.Union(lstNotExisted);
                    artModel.Tags = string.Join(",", lstTag.Select(n => n.TagName));
                }
                artModel.CreateBy = CurrentInstance.Instance.CurrentUser.UserName;
                artModel.CreateDate = DateTime.Now;
                artModel.URL = "/" + UnsignToString(artModel.Name);
                artSrv.CreateNew(artModel);
                artSrv.CommitChange();
                artSrv.CommitTran();
                AlertSuccess(InfoString.CREATE_SUCCESSFULL);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                artSrv.RollbackTran();
                LogError(ex);
                var model = new ArticleInputModel();
                var articleModel = artModel;
                model.ArticleModel = articleModel;
                var map = tags.Distinct().ToDictionary(x => StringUtil.UnsignToString(x));
                var lstTag = new List<Tag>();
                foreach (var item in map)
                {
                    var flag = tagSrv.CheckTitleContain(item.Key);
                    if (!flag)
                    {
                        var tag = new Tag();
                        tag.TagTitle = item.Key;
                        tag.TagName = item.Value;
                        lstTag.Add(tag);
                    }
                    else
                    {
                        lstTag.Add(tagSrv.GetByKey(item.Key));
                    }

                }
                model.Tags = lstTag;
                model.ParentCatId = catParentId;
                model.ParentCats = artCatSrv.GetParents();
                model.CatId = CatId;
                model.ChildCats = artCatSrv.GetChilds(catParentId);
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var artCatSrv = IoC.Resolve<IArticleCategoryService>();
            var artSrv = IoC.Resolve<IArticleService>();
            var tagSrv = IoC.Resolve<ITagService>();
            var model = new ArticleInputModel();
            var articleModel = artSrv.GetByKey(id);
            var catModel = artCatSrv.GetByKey(articleModel.CatId);
            model.ArticleModel = articleModel;
            model.ParentCatId = catModel.ParentId;
            model.ParentCats = artCatSrv.GetParents();
            model.CatId = articleModel.CatId;
            model.ChildCats = artCatSrv.GetChilds(catModel.ParentId);
            var lstTag = new List<Tag>();
            if (!string.IsNullOrEmpty(articleModel.Tags))
            {
                var str = articleModel.Tags.Split(',');
                foreach (var item in str.ToList())
                {
                    var key = StringUtil.UnsignToString(item);
                    lstTag.Add(tagSrv.GetByKey(key));
                }
                model.Tags = lstTag;
            }
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Update(Arcticle artModel, int? catParentId, int CatId, string[] tags)
        {
            var artSrv = IoC.Resolve<IArticleService>();
            var artCatSrv = IoC.Resolve<IArticleCategoryService>();
            var tagSrv = IoC.Resolve<ITagService>();
            var lstExisted = new List<Tag>();
            var lstNotExisted = new List<Tag>();
            artCatSrv.BeginTran();
            try
            {
                if (tags.Length > 0)
                {
                    var map = tags.Distinct().ToDictionary(x => StringUtil.UnsignToString(x));

                    foreach (var item in map)
                    {
                        var flag = tagSrv.CheckTitleContain(item.Key);
                        if (!flag)
                        {
                            var tagEntity = new Tag();
                            tagEntity.TagTitle = item.Key;
                            tagEntity.TagName = item.Value;
                            tagEntity.CreateBy = CurrentInstance.Instance.CurrentUser.UserName;
                            tagEntity.CreateDate = DateTime.Now;
                            tagEntity.Status = true;
                            tagSrv.CreateNew(tagEntity);
                            tagSrv.CommitChange();
                            lstNotExisted.Add(tagEntity);
                        }
                        else
                        {
                            lstExisted.Add(tagSrv.GetByKey(item.Key));
                        }
                    }

                    var lstTag = lstExisted.Union(lstNotExisted);
                    artModel.Tags = string.Join(",", lstTag.Select(n => n.TagName));
                }
                artModel.UpdateBy = CurrentInstance.Instance.CurrentUser.UserName;
                artModel.UpdateDate = DateTime.Now;
                artModel.URL = "/" + UnsignToString(artModel.Name);
                artSrv.Update(artModel);
                artSrv.CommitChange();
                artSrv.CommitTran();
                AlertSuccess(InfoString.UPDATE_SUCCESSFULL);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                artSrv.RollbackTran();
                LogError(ex);
                var model = new ArticleInputModel();
                var articleModel = artModel;
                model.ArticleModel = articleModel;
                var map = tags.Distinct().ToDictionary(x => StringUtil.UnsignToString(x));
                var lstTag = new List<Tag>();
                foreach (var item in map)
                {
                    var flag = tagSrv.CheckTitleContain(item.Key);
                    if (!flag)
                    {
                        var tag = new Tag();
                        tag.TagTitle = item.Key;
                        tag.TagName = item.Value;
                        lstTag.Add(tag);
                    }
                    else
                    {
                        lstTag.Add(tagSrv.GetByKey(item.Key));
                    }

                }
                model.Tags = lstTag;
                model.ParentCatId = catParentId;
                model.ParentCats = artCatSrv.GetParents();
                model.CatId = CatId;
                model.ChildCats = artCatSrv.GetChilds(catParentId);
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var artSrv = IoC.Resolve<IArticleService>();
                artSrv.Delete(id);
                artSrv.CommitChange();
                AlertSuccess(InfoString.DELETE_SUCCESSFULL);
            }
            catch (Exception ex)
            {
                LogError(ex);
                AlertError(InfoString.ERROR_SYSTEM);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult LoadChildCats(int? catParentId)
        {
            var artCatSrv = IoC.Resolve<IArticleCategoryService>();
            var chids = artCatSrv.GetChilds(catParentId);

            return Json(new
            {
                data = chids
            });
        }

        [HttpPost]
        public JsonResult GetTags(string keyword, int? page)
        {
            var tagSrv = IoC.Resolve<ITagService>();
            var currentPage = page.HasValue ? page.Value - 1 : 0;
            int total = 0;
            var lst = tagSrv.GetTags(keyword, currentPage, 10, out total);
            return Json(new
            {
                data = lst,
                total = total,
                JsonRequestBehavior.AllowGet
            });
        }
    }
}