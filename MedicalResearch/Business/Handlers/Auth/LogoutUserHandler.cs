using MediatR;
using MedicalResearch.Business.Commands.Auth;
using MedicalResearch.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Auth
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

            return Unit.Value;
        }
    }
}
