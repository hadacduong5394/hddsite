using HD.Domain.Models;
using MvcPaging;
using System.Collections.Generic;

namespace HD.Site.Areas.Admin.Models.ViewModel
{
    public class ArticleCatIndexModel
    {
        public int? CatId { get; set; }

        public string Name { get; set; }

        public List<ArcticleCategory> Parents { get; set; }

        public PagedList<ArcticleCategory> ArticleCategories { get; set; }
    }

    public class ArticleCategoryViewModel
    {
        public string Name { get; set;}

        public string URL { get; set;}

        public string Parent { get; set; }

        public string CreateBy { get; set; }

        public string CreateDate { get; set; }

        public string UpdateBy { get; set; }

        public string UpdateDate { get; set; }
    }
}