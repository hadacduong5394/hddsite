using HD.Domain.Models;
using MvcPaging;
using System.Collections.Generic;

namespace HD.Site.Areas.Admin.Models.ViewModel
{
    public class ArticleIndexModel
    {
        public int? ParentCatId { get; set; }

        public int? CatId { get; set; }

        public string KeyWord { get; set; }

        public List<ArcticleCategory> ArtCats { get; set; }

        public List<ArcticleCategory> Cats { get; set; }

        public PagedList<Arcticle> Articles { get; set; }
    }

    public class ArticleInputModel
    {
        public int? ParentCatId { get; set; }

        public int? CatId { get; set; }

        public List<ArcticleCategory> ParentCats { get; set; }

        public List<ArcticleCategory> ChildCats { get; set; }

        public Arcticle ArticleModel { get; set; }

        public List<Tag> Tags { get; set; }

        public ArticleInputModel()
        {
            Tags = new List<Tag>();
        }
    }
}