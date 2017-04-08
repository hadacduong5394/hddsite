using HD.Context;
using HD.Infrastructure.Repository;
using System.Collections.Generic;

namespace HD.IdentityManager.IRepository
{
    public interface IUserGroupRepository : IRepositoryBase<UserGroup, int>
    {
        IList<UserGroup> GetUsersByGroupId(int groupId);
    }
}