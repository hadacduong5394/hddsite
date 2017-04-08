using System.Linq;
using System.Collections.Generic;
using HD.Domain.Models;
using HD.Infrastructure.DBFactory;
using HD.Infrastructure.Repository;
using HD.Repository.Interface;

namespace HD.Repository.Implementation
{
    public class ArticleRepository : RepositoryBase<Arcticle, int>, IArticleRepository
    {
        public ArticleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Arcticle> GetByFilter(string keyWord, int? parentCatId, int? catId, int currentPage, int pageSize, out int total)
        {
            var query = from a in DbContext.Arcticles select a;

            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                query = query.Where(n => n.Name.Contains(keyWord) || n.Tittle.Contains(keyWord) || n.Tags.Contains(keyWord));
            }

            if (parentCatId.HasValue)
            {
                var lstChilds = (from a in DbContext.ArcticleCategories where a.ParentId == parentCatId.Value select a).Select(n => n.Id).ToArray();
                query = query.Where(n => lstChilds.Contains(n.CatId));
            }

            if (catId.HasValue)
            {
                query = query.Where(n => n.CatId == catId.Value);
            }

            query = query.OrderByDescending(n => n.Id);

            var max = currentPage <= 1 ? 4 - currentPage : 2;
            var rows = query.Skip(currentPage * pageSize).Take(pageSize * max + 1).ToList();
            total = currentPage * pageSize + rows.Count;

            var result = rows.Take(pageSize);

            return result;
        }
    }
}