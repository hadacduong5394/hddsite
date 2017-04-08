using HD.Context;
using HD.Core;
using HD.IdentityManager.IService;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HD.IdentityManager
{
    public class RBACAuthorized : AuthorizeAttribute
    {
        public RBACAuthorized() : base()
        {
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var currentUser = CurrentInstance.Instance.CurrentUser;
            var groupsOfUser = IoC.Resolve<IGroupService>().GetGroupIdsByUserId(currentUser.Id);
            var lstRoles = new List<Role>();
            foreach (var group in groupsOfUser)
            {
                var roleIds = IoC.Resolve<IRoleService>().GetRolesByGroupId(group.GroupId);
                foreach (var r in roleIds)
                {
                    var role = IoC.Resolve<IRoleService>().GetByKey(r.RoleId);
                    lstRoles.Add(role);
                }
            }

            var result = lstRoles.Distinct();

            if (result.Any(n => n.Name.Equals(this.Roles)))
            {
                return true;
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/NonAuthen/NonAutithencation.cshtml"
            };
        }
    }
}