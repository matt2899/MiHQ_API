using CodePulse.API.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace CodePulse.API.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(ApplicationUser user, List<string> roles);
    }
}
