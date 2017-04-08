using HD.Domain.Models;
using HD.Infrastructure.Repository;
using System.Collections.Generic;

namespace HD.Repository.Interface
{
    public interface IProductCategoryRepository : IRepositoryBase<ProductCategory, int>
    {
        IList<ProductCategory> GetByFilter(string keyWord, int? typeId, int? parentId, int currentPage, int pageSize, out int total);
    }
}