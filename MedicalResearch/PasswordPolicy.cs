using MedicalResearch.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalResearch
{
	public class PasswordPolicy : PasswordValidator<User>
	{
		public override async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
		{
			IdentityResult result = await base.ValidateAsync(manager, user, password);
			List<IdentityError> errors = result.Succeeded ? new List<IdentityError>() : result.Errors.ToList();

			if (!password.Any(char.IsLetter))
			{
				errors.Add(new IdentityError
				{
					Code = "PasswordRequiresLetter",
					Description = "Password must contain at least one letter"
				});
			}
			return errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
		}
	}
}