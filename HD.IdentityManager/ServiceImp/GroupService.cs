using HD.Context;
using HD.IdentityManager.IRepository;
using HD.IdentityManager.IService;
using HD.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace HD.IdentityManager.ServiceImp
{
    public class GroupService : BaseService, IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IRoleGroupRepository _roleGroupRepository;

        public GroupService(IUnitOfWork unitOfWork, IGroupRepository groupRepository, IUserGroupRepository userGroupRepository, IRoleGroupRepository roleGroupRepository) : base(unitOfWork)
        {
            this._groupRepository = groupRepository;
            this._userGroupRepository = userGroupRepository;
            this._roleGroupRepository = roleGroupRepository;
        }

        public void AddUserToGroup(string userId, int groupId)
        {
            var userGroupModel = new UserGroup();
            userGroupModel.UserId = userId;
            userGroupModel.GroupId = groupId;
            _userGroupRepository.CreateNew(userGroupModel);
        }

        public bool CheckContain(string groupName)
        {
            return _groupRepository.CheckContains(n => n.Name.ToUpper().Equals(groupName.ToUpper()));
        }

        public bool CheckContain(int groupId, string groupName)
        {
            return _groupRepository.CheckContains(n => n.Name.ToUpper().Equals(groupName.ToUpper()) && n.Id != groupId);
        }

        public void CreateNew(Group group)
        {
            _groupRepository.CreateNew(group);
        }

        public void Delete(int groupId)
        {
            _groupRepository.Delete(groupId);
        }

        public void DeleteRoleOfGroup(int groupId)
        {
            _roleGroupRepository.DeleteRoleOfGroup(groupId);
        }

        public IList<Group> GetAll()
        {
            return _groupRepository.GetAll().ToList();
        }

        public IList<Group> GetByFilter(string keyWord, int currentPage, int pageSize, out int totalRow)
        {
            return _groupRepository.GetByFilter(keyWord, currentPage, pageSize, out totalRow);
        }

        public Group GetByKey(int groupId)
        {
            return _groupRepository.GetSingleById(groupId);
        }

        public IList<UserGroup> GetUsersByGroupId(int groupId)
        {
            return _userGroupRepository.GetUsersByGroupId(groupId);
        }

        public void Update(Group group)
        {
            _groupRepository.Update(group);
        }

        public bool CheckGroupInAnyUser(int groupId)
        {
            return _userGroupRepository.CheckContains(n => n.GroupId == groupId);
        }

        public IList<UserGroup> GetGroupIdsByUserId(string userId)
        {
            return _userGroupRepository.GetMulti(n => n.UserId.Equals(userId)).ToList();
        }

        public void DeleteGroupOfUser(string userId)
        {
            _userGroupRepository.DeleteMulti(n => n.UserId.Equals(userId));
        }

        public bool CheckUserInAnyGroup(string userId)
        {
            return _userGroupRepository.CheckContains(n => n.UserId.Equals(userId));
        }
    }
}