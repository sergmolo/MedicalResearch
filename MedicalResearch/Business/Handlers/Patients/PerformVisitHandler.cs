using MediatR;
using MedicalResearch.Business.Commands.Patients;
using MedicalResearch.Business.Exceptions;
using MedicalResearch.Business.Pipeline;
using MedicalResearch.Data;
using MedicalResearch.Data.Entities;
using MedicalResearch.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Patients
{
    public class PerformVisitHandler : IRequestHandler<PerformVisitCommand>
    {
        private readonly ApplicationDbContext _dbContext;

        public PerformVisitHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(PerformVisitCommand request, CancellationToken ct)
        {
            var patient = await _dbContext.Patients.Include(p => p.Visits)
                    .SingleAsync(p => p.ClinicId == request.Model.ClinicId && p.PatientNumber == request.Model.PatientNumber, ct);

            if (patient.MedicineTypeId is null && patient.Status == PatientStatus.Screened)
            {
                patient.Status = PatientStatus.Randomized;
                var medTypes = await _dbContext.MedicineTypes.ToListAsync(ct);
                if (medTypes is null) throw new BusinessLogicException(CommandResult.Failed(new CommandError("NoMedicineTypes", "No medicine types")), 404);
                int randomInt = new Random().Next(0, medTypes.Count);
                patient.MedicineTypeId = medTypes[randomInt].Id;
            }

            var supply = await _dbContext.Supply.Include(s => s.Medicine)
                .FirstOrDefaultAsync(s => s.ClinicId == request.Model.ClinicId && s.ExpireAt > DateTime.UtcNow
                && s.Amount >= 1 && s.MedicineState == MedicineState.Ok
                && s.Medicine.MedicineTypeId == patient.MedicineTypeId, ct);

            if (supply is null)
            {
                throw new BusinessLogicException(
                    CommandResult.Failed(
                        new CommandError("MedicineForPatientNotFound", "Medicine for patient is not found")));
            }

            supply.Amount -= 1;
            patient.UpdatedAt = DateTime.UtcNow;
            patient.Visits.Add(new Visit()
            {
                Date = DateTime.UtcNow,
                MedicineId = supply.MedicineId,
                PatientId = patient.Id
            });

            await _dbContext.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
