using AuthorizationServer.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationServer.Infrastructure.Data
{
    public class AuthDbContext : IdentityDbContext<AppUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>(entity => entity.ToTable(name: "User"));
            builder.Entity<RefreshToken>(entity => entity.ToTable(name: "RefreshToken"));
            builder.Entity<IdentityRole>(entity => entity.ToTable(name: "Role"));
            builder.Entity<IdentityUserRole<string>>(entity => entity.ToTable(name: "UserRole"));
            builder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable(name: "UserLogin"));
            builder.Entity<IdentityUserToken<string>>(entity => entity.ToTable(name: "UserToken"));
            builder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable(name: "RoleClaim"));

            builder.Entity<AppUser>().HasData(
                    new AppUser
                    {
                        // Password : admin
                        Id = "60ca4504-70b4-4f67-98cd-3b5f3d0a6a34",
                        FirstName = "Ilya",
                        LastName = "Chvilyov",
                        UserName = "Administrator",
                        NormalizedUserName = "ADMINISTRATOR",
                        PasswordHash = "AQAAAAIAAYagAAAAEEEN2VuOPy1JZtmpQnRghCbta5V7erPq24Df58DPZzFABHSTsiFIepWLXrAMm1pG7w==",
                        SecurityStamp = "UNUYCBKBTG7CYWXZEJ67XU5EOKY5ETZZ",
                        ConcurrencyStamp = "5ffbfaa6-41c2-4611-a1b8-f711473cc6f4",
                    }
                );

            builder.Entity<IdentityRole>().HasData(
                    new IdentityRole
                    {
                        Id = "1",
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        ConcurrencyStamp = "Stamp1"
                    },

                    new IdentityRole
                    {
                        Id = "2",
                        Name = "User",
                        NormalizedName = "USER",
                        ConcurrencyStamp = "Stamp2"
                    });

            builder.Entity<IdentityUserRole<string>>().HasData(

                new IdentityUserRole<string> { UserId = "60ca4504-70b4-4f67-98cd-3b5f3d0a6a34", RoleId = "1" }
                );

            builder.ApplyConfiguration(new AppUserConfiguration());
        }
    }
}
