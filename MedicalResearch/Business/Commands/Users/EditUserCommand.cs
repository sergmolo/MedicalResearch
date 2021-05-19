using MediatR;
using MedicalResearch.Business.Models;
using MedicalResearch.V1.Requests;

namespace MedicalResearch.Business.Commands.Users
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
