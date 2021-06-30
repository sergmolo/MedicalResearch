using MediatR;
using MedicalResearch.V1.Requests;

namespace MedicalResearch.Business.Commands.Patients
{
    public class PerformVisitCommand : IRequest<Unit>
    {
        public PatientRequest Model { get; }

        public PerformVisitCommand(PatientRequest model)
        {
            Model = model;
        }
    }
}
