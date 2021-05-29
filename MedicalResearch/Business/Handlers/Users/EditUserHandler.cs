using MediatR;
using MedicalResearch.Business.Commands.Users;
using MedicalResearch.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Users
{
    public class EditUserHandler : IRequestHandler<EditUserCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;

        public EditUserHandler(UserManager<User> userManager,
            IPasswordHasher<User> passwordHasher)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }

        public async Task<Unit> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            
            user.FirstName = request.Model.FirstName;
            user.LastName = request.Model.LastName;
            user.Initials = request.Model.Initials;
            user.UpdatedAt = DateTime.UtcNow;

            if (request.Model.NewPassword is not null)
            {
                user.PasswordCreatedAt = DateTime.UtcNow;
                user.PasswordHash = _passwordHasher.HashPassword(user, request.Model.NewPassword);
            }

            await _userManager.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
