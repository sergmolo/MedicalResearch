using MediatR;
using MedicalResearch.Business.Commands.Patients;
using MedicalResearch.Data;
using MedicalResearch.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Patients
{
    public class EndParticipationHandler : IRequestHandler<EndParticipationCommand>
    {
        private readonly ApplicationDbContext _dbContext;

        public EndParticipationHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(EndParticipationCommand request, CancellationToken ct)
        {
            var patient = await _dbContext.Patients
                    .FirstOrDefaultAsync(p => p.ClinicId == request.Model.ClinicId 
                        && p.PatientNumber == request.Model.PatientNumber, ct);

            patient.Status = PatientStatus.FinishedEarly;

            await _dbContext.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
