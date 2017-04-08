using HD.Domain.Models;
using MvcPaging;

namespace HD.Site.Areas.Admin.Models.ViewModel
{
    public class TypeCategoryIndexModel
    {
        public string KeyWord { get; set; }

        public PagedList<TypeCategory> TypeCategories { get; set; }
    }
}