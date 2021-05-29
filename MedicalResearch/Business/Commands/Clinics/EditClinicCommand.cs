using MediatR;
using MedicalResearch.V1.Requests;

namespace MedicalResearch.Business.Commands.Clinics
{
    public class EditClinicCommand : IRequest<Unit>
    {
        public int Id { get; }
        public EditClinicRequest Model { get; }

        public EditClinicCommand(int id, EditClinicRequest model)
        {
            Id = id;
            Model = model;
        }
    }
}
