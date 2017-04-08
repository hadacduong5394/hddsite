using HD.Domain.Models;
using HD.Infrastructure.Repository;
using System.Collections.Generic;

namespace HD.Repository.Interface
{
    public interface IArticleRepository : IRepositoryBase<Arcticle, int>
    {
        IEnumerable<Arcticle> GetByFilter(string keyWord, int? parentCatId, int? catId, int currentPage, int pageSize, out int total);
    }
}