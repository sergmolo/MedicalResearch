using MediatR;
using MedicalResearch.Requests;

namespace MedicalResearch.Commands
{
	public class EditUserCommand : IRequest<CommandResult>
	{
		public EditUserRequest Model { get; }

		public EditUserCommand(EditUserRequest model)
		{
			Model = model;
		}
	}
}
