using HD.Context;
using HD.IdentityManager.IRepository;
using HD.IdentityManager.IService;
using HD.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace HD.IdentityManager.ServiceImp
{
    public class RoleService : BaseService, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleGroupRepository _roleGroupRepository;

        public RoleService(IUnitOfWork unitOfWork, IRoleRepository roleRepository, IRoleGroupRepository roleGroupRepository) : base(unitOfWork)
        {
            this._roleRepository = roleRepository;
            this._roleGroupRepository = roleGroupRepository;
        }

        public void AddRoleToGroup(int roleId, int groupId)
        {
            var roleGroupModel = new RoleGroup();
            roleGroupModel.RoleId = roleId;
            roleGroupModel.GroupId = groupId;
            _roleGroupRepository.CreateNew(roleGroupModel);
        }

        public bool CheckContain(string roleName)
        {
            return _roleRepository.CheckContains(n => n.Name.Equals(roleName));
        }

        public bool CheckContain(int roleId, string roleName)
        {
            return _roleRepository.CheckContains(n => n.Name.Equals(roleName) && n.Id != roleId);
        }

        public void CreateNew(Role role)
        {
            _roleRepository.CreateNew(role);
        }

        public void Delete(int roleId)
        {
            _roleRepository.Delete(roleId);
        }

        public IList<Role> GetAll()
        {
            return _roleRepository.GetAll().ToList();
        }

        public Role GetByKey(int roleId)
        {
            return _roleRepository.GetSingleById(roleId);
        }

        public IList<RoleGroup> GetRolesByGroupId(int groupId)
        {
            return _roleGroupRepository.GetRolesByGroupId(groupId);
        }

        public void Update(Role role)
        {
            _roleRepository.Update(role);
        }

        public IList<Role> GetByFilter(string roleName, int currentPage, int pageSize, out int totalRow)
        {
            return _roleRepository.GetByFilter(roleName, currentPage, pageSize, out totalRow);
        }

        public bool CheckRoleInAnyGroup(int roleId)
        {
            return _roleGroupRepository.CheckContains(n => n.RoleId == roleId);
        }
    }
}