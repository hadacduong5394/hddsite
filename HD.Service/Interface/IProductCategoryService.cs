using HD.Domain.Models;
using System.Collections.Generic;

namespace HD.Service.Interface
{
    public interface IProductCategoryService : IBaseService
    {
        IEnumerable<ProductCategory> GetParents();

        ProductCategory GetBykey(int id);

        void CreateNew(ProductCategory entity);

        void Update(ProductCategory entity);

        void Delete(int id);

        IList<ProductCategory> GetByFilter(string keyWord, int? typeId, int? parentId, int currentPage, int pageSize, out int total);
    }
}