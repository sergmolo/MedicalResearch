using MediatR;
using MedicalResearch.Business.Models;

namespace MedicalResearch.Business.Commands.Users
{
    public class RemoveUserByIdCommand : IRequest<CommandResult>
    {
        public int UserId { get; }

        public RemoveUserByIdCommand(int userId)
        {
            UserId = userId;
        }
    }
}
