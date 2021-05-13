using MediatR;
using MedicalResearch.Commands;
using MedicalResearch.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Handlers.Users
{
	public class EditUserHandler : IRequestHandler<EditUserCommand, CommandResult>
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<User> _userManager;
		private readonly IPasswordHasher<User> _passwordHasher;
		private readonly IPasswordValidator<User> _passwordValidator;

		public EditUserHandler(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager,
			IPasswordHasher<User> passwordHasher, IPasswordValidator<User> passwordValidator)
		{
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
			_passwordHasher = passwordHasher;
			_passwordValidator = passwordValidator;
		}

		public async Task<CommandResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
		{
			var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.FirstName = request.Model.FirstName;
			    user.LastName = request.Model.LastName;
			    user.Initials = request.Model.Initials;
				user.UpdatedAt = DateTime.UtcNow;

                if (request.Model.NewPassword is not null)
                {
                    IdentityResult result = await _passwordValidator.ValidateAsync(_userManager, user, request.Model.NewPassword);
					if (!result.Succeeded) return new CommandResult(Enums.CommandResults.wrong_password);
					
					user.PasswordCreatedAt = DateTime.UtcNow;
					user.PasswordHash = _passwordHasher.HashPassword(user, request.Model.NewPassword);
                }

				await _userManager.UpdateAsync(user);
			}
			return new CommandResult(0);
		}
	}
}
