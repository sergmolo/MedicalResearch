using MediatR;
using MedicalResearch.Commands;
using MedicalResearch.Models;
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
			User user = await _userManager.FindByIdAsync(userId);
			var isAdmin = _httpContextAccessor.HttpContext?.User?.IsInRole(Enums.Role.Administrator.ToString());
			if (isAdmin.HasValue && !isAdmin.Value)
			{
				user.IsRemoved = true;
				await _userManager.UpdateAsync(user);

				return new CommandResult(0);
			}

			return new CommandResult(99, "not ok, you are admin");
		}
	}
}
