using System.Linq;
using System.Collections.Generic;
using HD.Domain.Models;
using HD.Infrastructure.DBFactory;
using HD.Infrastructure.Repository;
using HD.Repository.Interface;
using System;

namespace HD.Repository.Implementation
{
    public class ArticleCategoryRepository : RepositoryBase<ArcticleCategory, int>, IArticleCategoryRepository
    {
        public ArticleCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public bool ExistChilds(int parentId)
        {
            var query = from a in DbContext.ArcticleCategories where a.ParentId == parentId select a;

            return query.Any();
        }

        public IList<ArcticleCategory> GetByFilter(int? catId, string name, int currentPage, int pageSize, out int total)
        {
            var query = from a in DbContext.ArcticleCategories select a;

            if (catId.HasValue)
            {
                query = query.Where(n => n.ParentId == catId.Value);
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(n => n.CatName.Contains(name));
            }

            query = query.OrderByDescending(n => n.Id);

            total = query.Count();

            var result = query.Skip(pageSize * currentPage).Take(pageSize).ToList();

            return result;
        }

        public List<ArcticleCategory> GetParents()
        {
            var query = from a in DbContext.ArcticleCategories where a.ParentId == null select a;

            return query.ToList();
        }
    }
}