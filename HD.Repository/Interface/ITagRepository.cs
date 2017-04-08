using HD.Domain.Models;
using HD.Infrastructure.Repository;
using System.Collections.Generic;

namespace HD.Repository.Interface
{
    public interface ITagRepository : IRepositoryBase<Tag, string>
    {
        IList<Tag> GetTags(string keyWord, int currentPage, int pageSize, out int total);
    }
}