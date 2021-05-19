using MediatR;
using MedicalResearch.Business.Commands.Users;
using MedicalResearch.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Handlers.Users
{
    public class LogoutUserHandler : IRequestHandler<LogoutUserCommand>
    {
        private readonly SignInManager<User> _signInManager;

        public LogoutUserHandler(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();
            return default;
        }
    }
}
