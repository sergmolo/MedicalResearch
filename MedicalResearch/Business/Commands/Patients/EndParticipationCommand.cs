using MediatR;
using MedicalResearch.V1.Requests;

namespace MedicalResearch.Business.Commands.Patients
{
    public class EndParticipationCommand : IRequest<Unit>
    {
        public EndParticipationRequest Model { get; }

        public EndParticipationCommand(EndParticipationRequest model)
        {
            Model = model;
        }
    }
}
