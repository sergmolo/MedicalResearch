using MediatR;
using MedicalResearch.V1.Requests;

namespace MedicalResearch.Business.Commands.Clinics
{
    public class AttachUserCommand : IRequest<Unit>
    {
        public int ClinicId { get; }
        public int UserId { get; }

        public AttachUserCommand(int clinicId, AttachUserRequest model)
        {
            ClinicId = clinicId;
            UserId = model.UserId;
        }
    }
}
