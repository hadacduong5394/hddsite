namespace HD.Context.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HD.Context.ContextConnection>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(HD.Context.ContextConnection context)
        {
            var checkAnyUser = from a in context.Users select a;
            if (!checkAnyUser.Any())
            {
                CreateUserDataSample(context);
            }
        }

        private void CreateUserDataSample(HD.Context.ContextConnection context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new HD.Context.ContextConnection()));

            var user = new ApplicationUser()
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Hà Đắc Dương",
                CreateDate = DateTime.Now,
                CreateBy = "admin",
                Status = true,
                Group = 1
            };
            manager.Create(user, "123456");
        }
    }
}
