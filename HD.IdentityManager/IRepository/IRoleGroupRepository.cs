using HD.Context;
using HD.Infrastructure.Repository;
using System.Collections.Generic;

namespace HD.IdentityManager.IRepository
{
    public interface IRoleGroupRepository : IRepositoryBase<RoleGroup, int>
    {
        IList<RoleGroup> GetRolesByGroupId(int groupId);

        void DeleteRoleOfGroup(int groupId);
    }
}