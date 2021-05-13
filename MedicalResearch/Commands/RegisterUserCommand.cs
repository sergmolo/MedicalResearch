using MediatR;
using MedicalResearch.Requests;
using Microsoft.AspNetCore.Identity;

namespace MedicalResearch.Commands
{

	public class RegisterUserCommand : IRequest<IdentityResult>
	{
		public RegisterRequest Model { get; }

		public RegisterUserCommand(RegisterRequest model)
		{
			Model = model;
		}
	}
}
