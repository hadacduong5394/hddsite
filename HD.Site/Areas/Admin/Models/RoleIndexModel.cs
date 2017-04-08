using HD.Context;
using MvcPaging;

namespace HD.Site.Areas.Admin.Models
{
    public class RoleIndexModel
    {
        public string KeyWord { get; set; }

        public IPagedList<Role> AppRoles { get; set; }
    }

    public class RolesModelDetail
    {
        public int Id { get; set; }

        public string Descreption { get; set; }

        public bool IsCheck { get; set; }
    }
}