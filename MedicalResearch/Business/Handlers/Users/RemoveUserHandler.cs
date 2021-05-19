using MediatR;
using MedicalResearch.Business.Commands.Users;
using MedicalResearch.Data.Entities;
using MedicalResearch.Data.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Handlers.Users
{
    public class RemoveUserHandler : IRequestHandler<RemoveUserCommand, CommandResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public RemoveUserHandler(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<CommandResult> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            var isAdmin = _httpContextAccessor.HttpContext?.User?.IsInRole(Role.Administrator.ToString());
            if (isAdmin.HasValue && !isAdmin.Value)
            {
                user.IsRemoved = true;
                await _userManager.UpdateAsync(user);

                return new CommandResult();
            }

            return new CommandResult(CommandError.YouAreAdmin);
        }
    }
}
