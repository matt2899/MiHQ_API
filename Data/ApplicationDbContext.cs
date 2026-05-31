using CodePulse.API.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var userRoleId = "cff7c87a-48c5-4967-a51f-6402cf408546";
            var adminRoleId = "37c35ff3-3d62-4dce-9eaa-ad3e278c7b4e";

            // Seed the roles
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = userRoleId
                },
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = adminRoleId
                }
            );

            var adminUserId = "836ece39-23c0-4dbe-a56a-b02ea446200f";
            // Create an Admin User
            var admin = new ApplicationUser()
            {
                Id = adminUserId,
                UserName = "admin@mihq.com",
                Email = "admin@mihq.com",
                FullName = "MiHQ",
                NormalizedEmail = "admin@mihq.com".ToUpper(),
                NormalizedUserName = "admin@mihq.com".ToUpper(),
                PasswordHash = "AQAAAAIAAYagAAAAEDwHPI+WHI/cA7oGXTSTqFq10fX/5U85qX2HScLuXSOBzaJdXorjaBEtScCrJt4tMg==",
                SecurityStamp = "ccbbbc6f-47e2-4fef-8cd6-c4e0219c97a2",
                ConcurrencyStamp = "e90b8ff3-e19c-4234-b9d2-d6b3c26eecfe"
            };


            builder.Entity<ApplicationUser>().HasData(admin);

            // Give roles to admin
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                },

            };
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }

    }



}
