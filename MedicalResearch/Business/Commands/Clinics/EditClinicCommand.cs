using MediatR;
using MedicalResearch.V1.Requests;

namespace MedicalResearch.Business.Commands.Clinics
{
    public class EditClinicCommand : IRequest<Unit>
    {
        public int Id { get; }
        public ClinicRequest Model { get; }

        public EditClinicCommand(int id, ClinicRequest model)
        {
            Id = id;
            Model = model;
        }
    }
}
