using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
    public class AuthDbContext: IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> option) : base(option)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "cff7c87a-48c5-4967-a51f-6402cf408546";
            var writerRoleId = "37c35ff3-3d62-4dce-9eaa-ad3e278c7b4e";


            // Create reader and writter role
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };

            // Seed the roles
            builder.Entity<IdentityRole>().HasData(roles);


            var adminUserId = "836ece39-23c0-4dbe-a56a-b02ea446200f";
            // Create an Admin User
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@mihq.com",
                Email = "admin@mihq.com",
                NormalizedEmail = "admin@mihq.com".ToUpper(),
                NormalizedUserName = "admin@mihq.com".ToUpper(),
                PasswordHash = "AQAAAAIAAYagAAAAEDwHPI+WHI/cA7oGXTSTqFq10fX/5U85qX2HScLuXSOBzaJdXorjaBEtScCrJt4tMg==",
                SecurityStamp = "ccbbbc6f-47e2-4fef-8cd6-c4e0219c97a2",
                ConcurrencyStamp = "e90b8ff3-e19c-4234-b9d2-d6b3c26eecfe"
            };

            
            builder.Entity<IdentityUser>().HasData(admin);

            // Give roles to admin
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

        }
    }
}
