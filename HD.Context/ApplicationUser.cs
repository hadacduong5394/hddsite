using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HD.Context
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(256)]
        public string FullName { get; set; }

        [MaxLength(256)]
        public string Address { get; set; }

        public DateTime? BirthDay { get; set; }

        [MaxLength(128)]
        [Column(TypeName = "varchar")]
        public string CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        [MaxLength(128)]
        [Column(TypeName = "varchar")]
        public string UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool Status { get; set; }

        public int Group { get; set; } //1-nhóm quản trị, 2-nhóm người dùng

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}