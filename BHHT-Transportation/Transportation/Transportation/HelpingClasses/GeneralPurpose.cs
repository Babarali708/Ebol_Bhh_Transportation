using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Transportation.Models;

namespace Transportation.HelpingClasses
{
    public class GeneralPurpose
    {
        private readonly HttpContext? hcontext;
        public GeneralPurpose(IHttpContextAccessor haccess)
        {
            hcontext = haccess.HttpContext;
        }

        public User? GetUserClaims()
        {
            string? userId = hcontext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
            string? firtName = hcontext?.User.Claims.FirstOrDefault(c => c.Type == "FirstName")?.Value;
            string? lastName = hcontext?.User.Claims.FirstOrDefault(c => c.Type == "LastName")?.Value;
            string? middleName = hcontext?.User.Claims.FirstOrDefault(c => c.Type == "MiddleName")?.Value;
            string? email = hcontext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            string? role = hcontext?.User.Claims.FirstOrDefault(c => c.Type == "Role")?.Value;

            User? loggedInUser = null;
            if (userId != null)
            {
                loggedInUser = new User()
                {
                    Id = int.Parse(userId),
                    FirstName = firtName,
                    LastName = lastName,
                    Email = email,
                    Role = Convert.ToInt32(role)
                };
            }

            return loggedInUser;
        }

        public async Task<bool> SetUserClaims(User user)
        {
            try
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("Role", user.Role.ToString()),
                };


                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await hcontext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                {
                    IsPersistent = true
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static DateTime DateTimeNow()
        {
            return DateTime.Now;
        }
    }
}
