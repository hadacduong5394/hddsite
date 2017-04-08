using HD.Context;
using Microsoft.AspNet.Identity;
using System.Web;

namespace HD.IdentityManager
{
    public class CurrentInstance
    {
        private static CurrentInstance instance;

        private CurrentInstance()
        {
        }

        public static CurrentInstance Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CurrentInstance();
                }
                return instance;
            }
        }

        private ApplicationUser _currentUser;

        public ApplicationUser CurrentUser
        {
            get
            {
                var userIdentity = HttpContext.Current.GetOwinContext().Authentication.User;
                var currentUser = UserManager.Instance.UserManagerment.FindByName(userIdentity.Identity.Name);

                return currentUser;
            }
            private set
            {
                _currentUser = value;
            }
        }
    }
}