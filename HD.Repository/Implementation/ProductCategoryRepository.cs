using System.Linq;
using System.Collections.Generic;
using HD.Domain.Models;
using HD.Infrastructure.DBFactory;
using HD.Infrastructure.Repository;
using HD.Repository.Interface;

namespace HD.Repository.Implementation
{
    public class ProductCategoryRepository : RepositoryBase<ProductCategory, int>, IProductCategoryRepository
    {
        public ProductCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IList<ProductCategory> GetByFilter(string keyWord, int? typeId, int? parentId, int currentPage, int pageSize, out int total)
        {
            var query = from a in DbContext.ProductCategories select a;

            if (!string.IsNullOrEmpty(keyWord))
            {
                query = query.Where(n => n.CatName.Contains(keyWord));
            }

            if (typeId.HasValue)
            {
                query = query.Where(n => n.TypeId == typeId.Value);
            }

            if (parentId.HasValue)
            {
                query = query.Where(n=>n.ParentId == parentId);
            }

            query = query.OrderByDescending(n=>n.Id);

            total = query.Count();

            var result = query.Skip(currentPage * pageSize).Take(pageSize).ToList();

            return result;
        }
    }
}