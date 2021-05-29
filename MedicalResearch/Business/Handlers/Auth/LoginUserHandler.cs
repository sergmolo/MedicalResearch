using MediatR;
using MedicalResearch.Business.Commands.Auth;
using MedicalResearch.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Auth
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public LoginUserHandler(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Model.Email);

            await _signInManager.SignInAsync(user, request.Model.RememberMe);

            return Unit.Value;
        }
    }
}
