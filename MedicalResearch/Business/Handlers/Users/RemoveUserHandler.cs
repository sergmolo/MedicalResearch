using MediatR;
using MedicalResearch.Business.Commands.Auth;
using MedicalResearch.Business.Commands.Users;
using MedicalResearch.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Users
{
    public class RemoveUserHandler : IRequestHandler<RemoveUserCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        public RemoveUserHandler(UserManager<User> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(RemoveUserCommand request, CancellationToken ct)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            user.IsRemoved = true;
            await _userManager.UpdateAsync(user);

            await _mediator.Send(new LogoutUserCommand(), ct);

            return Unit.Value;
        }
    }
}
