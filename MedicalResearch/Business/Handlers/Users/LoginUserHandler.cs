using MediatR;
using MedicalResearch.Business.Commands.Users;
using MedicalResearch.Data.Entities;
using MedicalResearch.Data.Enums;
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
            if (user == null) return new CommandResult(CommandError.UserNotFound);
            if (user.IsRemoved) return new CommandResult(CommandError.UserRemoved);

            if (!(await _signInManager.CheckPasswordSignInAsync(user, request.Model.Password, true)).Succeeded)
                return new CommandResult(CommandError.WrongEmailOrPassword);

            var PasswordExpireMonth = int.Parse(_configuration["PasswordExpireMonth"]);

            if (user.PasswordCreatedAt.AddMonths(PasswordExpireMonth) < DateTime.UtcNow)
            {
                return new CommandResult(CommandError.PasswordExpired);
            }

            var res = await _signInManager.PasswordSignInAsync(
                request.Model.Email,
                request.Model.Password,
                request.Model.RememberMe,
                false);

            if (res.Succeeded)
            {
                return new CommandResult();
            }
            
            return new CommandResult(CommandError.WrongEmailOrPassword);
        }
    }
}
