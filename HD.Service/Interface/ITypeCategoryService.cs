using HD.Domain.Models;
using System.Collections.Generic;

namespace HD.Service.Interface
{
    public interface ITypeCategoryService : IBaseService
    {
        IEnumerable<TypeCategory> GetAll();

        TypeCategory GetBykey(int id);

        void CreateNew(TypeCategory entity);

        void Update(TypeCategory entity);

        void Delete(int id);

        IList<TypeCategory> GetByfilter(string name, int currentPage, int pageSize, out int total);

        bool CheckContainProductCategory(int typeId);
    }
}