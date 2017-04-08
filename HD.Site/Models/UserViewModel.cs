using HD.Context;
using MvcPaging;
using System.Collections.Generic;

namespace HD.Site.Models
{
    public class UserViewModel
    {
        public string KeyWord { get; set; }

        public IPagedList<ApplicationUser> Users { get; set; }
    }

    public class UserModelDetail
    {
        public ApplicationUser User { get; set; }

        public IList<GroupVM> Groups { get; set; }
    }

    public class GroupVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsCheck { get; set; }

        public string Descreption { get; set; }
    }
}