using MediatR;
using MedicalResearch.Data.Enums;
using MedicalResearch.V1.Requests;

namespace MedicalResearch.Business.Commands.Users
{
    public class EditUserRoleCommand : IRequest<Unit>
    {
        public int UserId { get; }

        public Role Role { get; }

        public EditUserRoleCommand(int userId, EditUserRoleRequest request)
        {
            UserId = userId;
            Role = request.Role;
        }
    }
}
