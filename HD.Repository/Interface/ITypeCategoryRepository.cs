using HD.Domain.Models;
using HD.Infrastructure.Repository;
using System.Collections.Generic;

namespace HD.Repository.Interface
{
    public interface ITypeCategoryRepository : IRepositoryBase<TypeCategory, int>
    {
        IList<TypeCategory> GetByfilter(string name, int currentPage, int pageSize, out int total);
    }
}