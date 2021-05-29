using MediatR;
using MedicalResearch.V1.Requests;

namespace MedicalResearch.Business.Commands.Users
{
    public class LinkUserToClinicCommand : IRequest<Unit>
    {
        public int UserId { get; }
        public int ClinicId { get; }

        public LinkUserToClinicCommand(int userId, LinkUserToClinicRequest model)
        {
            UserId = userId;
            ClinicId = model.ClinicId;
        }
    }
}
