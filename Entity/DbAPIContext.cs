using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Demo.Entity
{
    public class DbAPIContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>,
                                                UserRoleMap, IdentityUserLogin<string>,
                                                IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DbAPIContext(DbContextOptions<DbAPIContext> options) : base(options)
        {

        }
        //public static DbAPIContext Create(DbContextOptions<DbAPIContext> options)
        //{
        //    return new DbAPIContext(options);
        //}
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            base.OnModelCreating(builder);

            builder.Entity<UserRoleMap>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoleMaps)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

                userRole.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoleMaps)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });

            builder.Entity<Role>().HasData(new Role
            {
                Id = "client",
                Name = "Client",
                NormalizedName = "CLIENT"
            }, new Role
            {
                Id = "admin",
                Name = "Admin",
                NormalizedName = "ADMIN"
            });

            
        }
    }
}
