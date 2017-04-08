using HD.Context;
using HD.Infrastructure.Repository;
using System.Collections.Generic;

namespace HD.IdentityManager.IRepository
{
    public interface IRoleRepository : IRepositoryBase<Role, int>
    {
        IList<Role> GetByFilter(string roleName, int currentPage, int pageSize, out int totalRow);
    }
}