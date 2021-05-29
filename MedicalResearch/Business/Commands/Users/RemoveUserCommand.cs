using MediatR;

namespace MedicalResearch.Business.Commands.Users
{
    public class RemoveUserCommand : IRequest<Unit>
    {
        public int UserId { get; }

        public RemoveUserCommand(int userId)
        {
            UserId = userId;
        }
    }
}
