using MediatR;
using MedicalResearch.Business.Models;
using MedicalResearch.V1.Requests;

namespace MedicalResearch.Business.Commands.Users
{
    public class EditUserCommand : IRequest<CommandResult>
    {
        public int UserId { get; }

        public EditUserRequest Model { get; }

        public EditUserCommand(int userId, EditUserRequest model)
        {
            UserId = userId;
            Model = model;
        }
    }
}
