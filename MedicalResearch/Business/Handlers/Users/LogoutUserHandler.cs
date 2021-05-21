using MediatR;
using MedicalResearch.Business.Commands.Users;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using MedicalResearch.Data.Entities;
using MedicalResearch.Business.Models;

namespace MedicalResearch.Business.Handlers.Users
{
    public class LogoutUserHandler : IRequestHandler<LogoutUserCommand, CommandResult>
    {
        private readonly SignInManager<User> _signInManager;

        public LogoutUserHandler(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<CommandResult> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();
            
            return CommandResult.Success();
        }
    }
}
