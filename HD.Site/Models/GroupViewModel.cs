using HD.Context;
using MvcPaging;
using System.Collections.Generic;

namespace HD.Site.Models
{
    public class GroupViewModel
    {
        public string KeyWord { get; set; }

        public IPagedList<Group> Groups { get; set; }
    }

    public class GroupModelDetail
    {
        public Group Group { get; set; }

        public List<RolesModelDetail> Roles { get; set; }
    }
}