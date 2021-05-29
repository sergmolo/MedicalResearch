using MediatR;
using MedicalResearch.V1.Requests;

namespace MedicalResearch.Business.Commands.Clinics
{
    public class AddClinicCommand : IRequest<Unit>
    {
        public AddClinicRequest Model { get; }

        public AddClinicCommand(AddClinicRequest model)
        {
            Model = model;
        }
    }
}
