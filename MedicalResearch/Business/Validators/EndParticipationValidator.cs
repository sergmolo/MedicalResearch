using FluentValidation;
using MedicalResearch.Data;
using MedicalResearch.Business.Commands.Patients;
using Microsoft.EntityFrameworkCore;

namespace MedicalResearch.Business.Validators
{
    public class EndParticipationValidator : AbstractValidator<EndParticipationCommand>
    {
        public EndParticipationValidator(ApplicationDbContext dbContext)
        {
            RuleFor(m => m.Model.ClinicId)
                .NotEmpty()
                .MustAsync(async (x, ct) => await dbContext.Patients.AsNoTracking().AnyAsync(p => p.ClinicId == x, ct));

            RuleFor(m => m.Model.PatientNumber)
                .NotEmpty()
                .MustAsync(async (x, ct) => await dbContext.Patients.AsNoTracking().AnyAsync(p => p.PatientNumber == x, ct));
        }
    }
}
