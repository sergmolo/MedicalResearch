using MediatR;
using MedicalResearch.Commands;
using MedicalResearch.Enums;
using MedicalResearch.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Handlers.Users
{
	public class LoginUserHandler : IRequestHandler<LoginUserCommand, CommandResult>
	{
		private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;
		private readonly IConfiguration _configuration;

		public LoginUserHandler(SignInManager<User> signInManager, UserManager<User> userManager, IConfiguration configuration)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_configuration = configuration;
		}

		public async Task<CommandResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByNameAsync(request.Model.Email);
			if (user == null) return new CommandResult(CommandResults.user_not_found);
			if (user.IsRemoved) return new CommandResult(CommandResults.user_removed);
			var PasswordExpireMonth = int.Parse(_configuration["PasswordExpireMonth"]);

			if (user.PasswordCreatedAt.AddMonths(PasswordExpireMonth) < DateTime.UtcNow)
			{
				return new CommandResult(101, "password expired");
			}

			var res = await _signInManager.PasswordSignInAsync(
				request.Model.Email,
				request.Model.Password,
				request.Model.RememberMe,
				false);

			if (res.Succeeded)
			{
				return new CommandResult(CommandResults.ok);
			}
			
			return new CommandResult(CommandResults.wrong_email_or_password);
		}
	}
}
