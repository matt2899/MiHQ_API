using Microsoft.AspNetCore.Identity;

namespace CodePulse.API.Models.Domain
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
