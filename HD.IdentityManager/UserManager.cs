using Microsoft.AspNet.Identity.Owin;
using System.Web;

namespace HD.IdentityManager
{
    public class UserManager
    {
        private static UserManager _instance;

        private UserManager()
        {
        }

        public static UserManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserManager();
                }
                return _instance;
            }
        }

        private ApplicationUserManager _userManagerment;

        public ApplicationUserManager UserManagerment
        {
            get
            {
                return _userManagerment ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManagerment = value;
            }
        }
    }
}