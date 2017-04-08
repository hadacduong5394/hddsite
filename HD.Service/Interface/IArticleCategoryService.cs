using HD.Domain.Models;
using System.Collections.Generic;

namespace HD.Service.Interface
{
    public interface IArticleCategoryService : IBaseService
    {
        IList<ArcticleCategory> GetByFilter(int? catId, string name, int currentPage, int pageSize, out int total);

        void CreateNew(ArcticleCategory arcticleCat);

        void Update(ArcticleCategory arcticleCat);

        void Delete(int articleCatId);

        ArcticleCategory GetByKey(int id);

        List<ArcticleCategory> GetParents();

        List<ArcticleCategory> GetChilds(int? parentId);

        bool ExistChilds(int parentId);
    }
}