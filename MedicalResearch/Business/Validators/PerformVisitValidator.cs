using FluentValidation;
using MedicalResearch.Business.Commands.Patients;
using MedicalResearch.Data;
using MedicalResearch.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace MedicalResearch.Business.Validators
{
    public class PerformVisitValidator : AbstractValidator<PerformVisitCommand>
    {
        public PerformVisitValidator(ApplicationDbContext dbContext)
        {
            RuleFor(m => m.Model.ClinicId)
                .NotEmpty()
                .MustAsync(async (clinicId, ct) => await dbContext.Patients.AsNoTracking()
                        .AnyAsync(p => p.ClinicId == clinicId, ct));

            RuleFor(m => m.Model.PatientNumber)
                .NotEmpty()
                .MustAsync(async (patientNumber, ct) => await dbContext.Patients.AsNoTracking()
                        .AnyAsync(p => p.PatientNumber == patientNumber, ct));

            RuleFor(m => m.Model)
                .NotEmpty()
                .MustAsync(async (x, ct) =>
                    {
                        var patient = await dbContext.Patients.AsNoTracking()
                            .SingleAsync(p => p.ClinicId == x.ClinicId && p.PatientNumber == x.PatientNumber, ct);
                        return patient.Status != PatientStatus.Finished && patient.Status != PatientStatus.FinishedEarly;
                    })
                .WithMessage("Participation ended");
        }
    }
}
