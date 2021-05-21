using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MedicalResearch.Business.Services
{
    public class PasswordPolicy<TUser> : PasswordValidator<TUser> where TUser : class
    {
        public override async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            var result = await base.ValidateAsync(manager, user, password);
            var errors = result.Succeeded ? new List<IdentityError>() : result.Errors.ToList();

            if (!password.Any(char.IsLetter))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordRequiresLetter",
                    Description = "Password must contain at least one letter"
                });
            }

            return !errors.Any() ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }
    }
}
