using HD.Context;
using System.Collections.Generic;

namespace HD.IdentityManager.IService
{
    public interface IRoleService : IBaseService
    {
        void CreateNew(Role role);

        void Update(Role role);

        void Delete(int roleId);

        bool CheckContain(string roleName);

        bool CheckContain(int roleId, string roleName);

        Role GetByKey(int roleId);

        IList<Role> GetAll();

        IList<RoleGroup> GetRolesByGroupId(int groupId);

        void AddRoleToGroup(int roleId, int groupId);

        IList<Role> GetByFilter(string roleName, int currentPage, int pageSize, out int totalRow);

        bool CheckRoleInAnyGroup(int roleId);
    }
}