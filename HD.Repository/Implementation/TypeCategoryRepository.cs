using System.Linq;
using System.Collections.Generic;
using HD.Domain.Models;
using HD.Infrastructure.DBFactory;
using HD.Infrastructure.Repository;
using HD.Repository.Interface;

namespace HD.Repository.Implementation
{
    public class TypeCategoryRepository : RepositoryBase<TypeCategory, int>, ITypeCategoryRepository
    {
        public TypeCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IList<TypeCategory> GetByfilter(string name, int currentPage, int pageSize, out int total)
        {
            var query = from a in DbContext.TypeCategories select a;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(n => n.Name.Contains(name));
            }

            query = query.OrderByDescending(n => n.Id);

            total = query.Count();

            var lst = query.Skip(currentPage * pageSize).ToList();

            return lst;
        }
    }
}