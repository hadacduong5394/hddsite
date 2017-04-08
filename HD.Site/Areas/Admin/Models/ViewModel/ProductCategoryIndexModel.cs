using HD.Domain.Models;
using MvcPaging;
using System.Collections.Generic;

namespace HD.Site.Areas.Admin.Models.ViewModel
{
    public class ProductCategoryIndexModel
    {
        public string KeyWord { get; set; }

        public int? TypeId { get; set; }

        public int? ParentId { get; set; }

        public IEnumerable<TypeCategory> TypeCats { get; set; }

        public IEnumerable<ProductCategory> ProductCatParents { get; set; }

        public IPagedList<ProductCategory> ProductCats { get; set; }
    }
}