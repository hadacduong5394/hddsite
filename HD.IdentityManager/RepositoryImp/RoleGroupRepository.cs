using HD.Context;
using HD.IdentityManager.IRepository;
using HD.Infrastructure.DBFactory;
using HD.Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;

namespace HD.IdentityManager.RepositoryImp
{
    public class RoleGroupRepository : RepositoryBase<RoleGroup, int>, IRoleGroupRepository
    {
        public RoleGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public void DeleteRoleOfGroup(int groupId)
        {
            DeleteMulti(n => n.GroupId == groupId);
        }

        public IList<RoleGroup> GetRolesByGroupId(int groupId)
        {
            var query = from a in DbContext.RoleGroups where a.GroupId == groupId select a;

            query = query.OrderBy(n => n.RoleId);

            return query.ToList();
        }
    }
}