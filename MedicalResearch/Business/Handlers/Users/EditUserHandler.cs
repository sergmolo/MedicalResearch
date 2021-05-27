using MediatR;
using MedicalResearch.Business.Commands.Users;
using MedicalResearch.Business.Enums;
using MedicalResearch.Business.Models;
using MedicalResearch.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Users
{
    public class EditUserHandler : IRequestHandler<EditUserByIdCommand, CommandResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IPasswordValidator<User> _passwordValidator;

        public EditUserHandler(UserManager<User> userManager,
            IPasswordHasher<User> passwordHasher, 
            IPasswordValidator<User> passwordValidator)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _passwordValidator = passwordValidator;
        }

        public async Task<CommandResult> Handle(EditUserByIdCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            
            user.FirstName = request.Model.FirstName;
            user.LastName = request.Model.LastName;
            user.Initials = request.Model.Initials;
            user.UpdatedAt = DateTime.UtcNow;

            if (request.Model.NewPassword is not null)
            {
                var result = await _passwordValidator.ValidateAsync(_userManager, user, request.Model.NewPassword);
                if (!result.Succeeded)
                {
                    return CommandResult.Failed(CommandErrorCode.WrongPassword);
                }

                user.PasswordCreatedAt = DateTime.UtcNow;
                user.PasswordHash = _passwordHasher.HashPassword(user, request.Model.NewPassword);
            }

            await _userManager.UpdateAsync(user);

            return CommandResult.Success();
        }
    }
}
