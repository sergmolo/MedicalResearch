using MediatR;
using MedicalResearch.Requests;

namespace MedicalResearch.Commands
{
	public class LoginUserCommand : IRequest<CommandResult>
	{
		public LoginRequest Model { get; }

		public LoginUserCommand(LoginRequest model)
		{
			Model = model;
		}
	}
}
