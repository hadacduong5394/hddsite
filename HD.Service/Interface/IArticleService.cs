using HD.Domain.Models;
using System.Collections.Generic;

namespace HD.Service.Interface
{
    public interface IArticleService : IBaseService
    {
        IEnumerable<Arcticle> GetByFilter(string keyWord, int? parentCatId, int? catId, int currentPage, int pageSize, out int total);

        void CreateNew(Arcticle article);

        void Update(Arcticle article);

        void Delete(int articleId);

        Arcticle GetByKey(int id);
    }
}