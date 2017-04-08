using HD.Core;
using HD.Domain.Models;
using HD.Service.Interface;
using HD.Site.Areas.Admin.Models.ViewModel;
using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HD.Site.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductCategoryController : BaseController
    {
        public ProductCategoryController()
        {

        }

        // GET: Admin/ProductCategory
        public ActionResult Index(ProductCategoryIndexModel model, int? page)
        {
            var proCatSrv = IoC.Resolve<IProductCategoryService>();
            var typeCatSrv = IoC.Resolve<ITypeCategoryService>(); 

            int currentPage = page.HasValue ? page.Value - 1 : 0;
            int pageSize = 10;
            int total = 0;
            var lst = proCatSrv.GetByFilter(model.KeyWord, model.TypeId, model.ParentId, currentPage, pageSize, out total);
            model.ProductCats = new PagedList<ProductCategory>(lst, currentPage, pageSize, total);
            model.TypeCats = typeCatSrv.GetAll();
            model.ProductCatParents = proCatSrv.GetParents();
            return View(model);
        }
    }
}