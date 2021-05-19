using System.Security.Claims;
using System.Threading.Tasks;
using MedicalResearch.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace MedicalResearch.Business.Services
{
    public class ClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
    {
        public ClaimsPrincipalFactory(UserManager<User> userManager, IOptions<IdentityOptions> options)
            : base(userManager, options)
        {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            ClaimsPrincipal principal = await base.CreateAsync(user);
            
            var role = user.Role.ToString();
            if (principal?.Identity is not null)
            {
                ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return principal!;
        }
    }
}
