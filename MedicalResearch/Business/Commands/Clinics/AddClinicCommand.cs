using MediatR;
using MedicalResearch.V1.Requests;

namespace MedicalResearch.Business.Commands.Clinics
{
    public class AddClinicCommand : IRequest<Unit>
    {
        public ClinicRequest Model { get; }

        public AddClinicCommand(ClinicRequest model)
        {
            Model = model;
        }
    }
}
