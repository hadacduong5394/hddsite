using HD.Context;
using HD.Infrastructure.Repository;
using System.Collections.Generic;

namespace HD.IdentityManager.IRepository
{
    public interface IGroupRepository : IRepositoryBase<Group, int>
    {
        IList<Group> GetByFilter(string keyWord, int currentPage, int pageSize, out int totalRow);
    }
}