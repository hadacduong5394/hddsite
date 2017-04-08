using HD.Context;
using System.Collections.Generic;

namespace HD.IdentityManager.IService
{
    public interface IGroupService : IBaseService
    {
        void CreateNew(Group group);

        void Update(Group group);

        void Delete(int groupId);

        bool CheckContain(string groupName);

        bool CheckContain(int groupId, string groupName);

        Group GetByKey(int groupId);

        IList<Group> GetAll();

        IList<UserGroup> GetUsersByGroupId(int groupId);

        void AddUserToGroup(string userId, int groupId);

        IList<Group> GetByFilter(string keyWord, int currentPage, int pageSize, out int totalRow);

        void DeleteRoleOfGroup(int groupId);

        bool CheckGroupInAnyUser(int groupId);

        IList<UserGroup> GetGroupIdsByUserId(string userId);

        void DeleteGroupOfUser(string userId);

        bool CheckUserInAnyGroup(string userId);
    }
}