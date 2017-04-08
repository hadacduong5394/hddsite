using HD.Domain.Models;
using HD.Infrastructure.Repository;
using System.Collections.Generic;

namespace HD.Repository.Interface
{
    public interface IArticleCategoryRepository : IRepositoryBase<ArcticleCategory, int>
    {
        IList<ArcticleCategory> GetByFilter(int? catId, string name, int currentPage, int pageSize, out int total);

        List<ArcticleCategory> GetParents();

        bool ExistChilds(int parentId);
    }
}