using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<ApplicationUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            // Check email
            var identityUser = await userManager.FindByEmailAsync(request.Email);

            if (identityUser is not null)
            {

                var roles = await userManager.GetRolesAsync(identityUser);
                // Check password
               var checkPasswordResult = await userManager.CheckPasswordAsync(identityUser, request.Password);

                if (checkPasswordResult)
                {
                    // Create token
                    var jwtToken = tokenRepository.CreateJwtToken(identityUser, roles.ToList());

                    var response = new LoginResponseDto
                    { 
                        Email = request.Email,
                        Roles = roles.ToList(),
                        
                    };

                    Response.Cookies.Append("access-token", jwtToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTime.UtcNow.AddMinutes(15)
                    });

                    return Ok(response);
                }
            }
            ModelState.AddModelError("", "Email or Password Incorrect");

            return ValidationProblem(ModelState);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            // Create identity user object
            var user = new ApplicationUser
            {
                UserName = request.Email?.Trim(),
                Email = request.Email?.Trim(),
                FullName = request?.FullName,
                Bio = request?.Bio,
                ProfileImageUrl= request?.ProfileImageUrl
            };

            // Create User
            var identityResult = await userManager.CreateAsync(user, request.Password);

            if (identityResult.Succeeded)
            {
                // Add Role to user (User)
                identityResult = await userManager.AddToRoleAsync(user, "User");

                if (identityResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    if (identityResult.Errors.Any())
                    {
                        foreach (var error in identityResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            else
            {
                if(identityResult.Errors.Any())
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return ValidationProblem(ModelState);

        }


        [HttpGet]
        [Route("me")]
        [Authorize]
        public async Task<IActionResult> UserDetails()
        {
            if (User.Identity is null || !User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var response = new LoginResponseDto
            {
                Email = User.FindFirst(ClaimTypes.Email)?.Value,
                Roles = User.FindAll(ClaimTypes.Role).Select(x => x.Value).ToList()
            };

            return Ok(response);
        }

        [HttpPost]
        [Route("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Append("access-token", "", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddDays(-1)
            });

            return Ok();
        }
    }
}
