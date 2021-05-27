using MediatR;

namespace MedicalResearch.Business.Commands.Users
{
    public class RemoveUserByIdCommand : IRequest<Unit>
    {
        public int UserId { get; }

        public RemoveUserByIdCommand(int userId)
        {
            UserId = userId;
        }
    }
}
