using MediatR;
using MedicalResearch.Business.Commands.Users;
using MedicalResearch.Business.Enums;
using MedicalResearch.Business.Models;
using MedicalResearch.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Users
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, CommandResult>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly AuthOptions _authOptions;

        public LoginUserHandler(SignInManager<User> signInManager, UserManager<User> userManager, IOptions<AuthOptions> options)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _authOptions = options.Value;
        }

        public async Task<CommandResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Model.Email);
            if (user is null)
            {
                return CommandResult.Failed(CommandErrorCode.UserNotFound);
            }
            if (user.IsRemoved) {
                return CommandResult.Failed(CommandErrorCode.UserRemoved);
            }
            if (await _userManager.IsLockedOutAsync(user))
            {
                return CommandResult.Failed(CommandErrorCode.UserIsLockedOut);
            }

            var passwordCheck = (await _signInManager.CheckPasswordSignInAsync(user, request.Model.Password, true)).Succeeded;
            if (!passwordCheck)
            {
                return CommandResult.Failed(CommandErrorCode.WrongPassword);
            }

            var passwordExpireMonth = _authOptions.PasswordExpireMonth;
            if (user.PasswordCreatedAt.AddMonths(passwordExpireMonth) < DateTime.UtcNow)
            {
                return CommandResult.Failed(CommandErrorCode.PasswordExpired);
            }

            await _signInManager.SignInAsync(user, request.Model.RememberMe);

            return CommandResult.Success();
        }
    }
}
