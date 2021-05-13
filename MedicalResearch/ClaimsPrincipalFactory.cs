using MedicalResearch.Enums;
using MedicalResearch.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedicalResearch
{
    public class ClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, ApplicationRole>
    {
        public ClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<ApplicationRole> roleManager, IOptions<IdentityOptions> options) 
            : base(userManager, roleManager, options)
        {
        }

        public async override Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var principal = await base.CreateAsync(user);

            var role = ((Role)user.RoleId).ToString();
            if (principal.Identity is not null)
            {
                ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.Role, role));

            }

            return principal!;
        }
    }
}
