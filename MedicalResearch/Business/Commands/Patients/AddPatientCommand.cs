using MediatR;
using MedicalResearch.V1.Requests;

namespace MedicalResearch.Business.Commands.Patients
{
    public class AddPatientCommand : IRequest<Unit>
    {
        public AddPatientRequest Model { get; }

        public AddPatientCommand(AddPatientRequest model)
        {
            Model = model;
        }
    }
}
