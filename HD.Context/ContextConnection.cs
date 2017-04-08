using HD.Domain.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace HD.Context
{
    public class ContextConnection : IdentityDbContext<ApplicationUser>
    {
        public ContextConnection() : base("ConnectionString")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Error> Errors { get; set; }
        public DbSet<Arcticle> Arcticles { get; set; }
        public DbSet<ArcticleCategory> ArcticleCategories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TypeCategory> TypeCategories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Role> ApplicationRoles { get; set; }
        public DbSet<RoleGroup> RoleGroups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }

        public static ContextConnection Create()
        {
            return new ContextConnection();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole>().HasKey(n => new { n.UserId, n.RoleId }).ToTable("ApplicationUserRoles");
            modelBuilder.Entity<IdentityUserLogin>().HasKey(n => n.UserId).ToTable("ApplicationUserLogins");
            modelBuilder.Entity<IdentityUserClaim>().HasKey(n => n.UserId).ToTable("ApplicationUserClaims");

            //modelBuilder.Entity<Arcticle>().HasRequired(n => n.ArcticleCat).WithMany(n => n.Articles).HasForeignKey(n => n.CatId);
        }
    }
}