using FluentValidation;
using MedicalResearch.Business.Commands.Patients;
using MedicalResearch.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace MedicalResearch.Business.Validators
{
    public class AddPatientValidator : AbstractValidator<AddPatientCommand>
    {
        public AddPatientValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Model.PatientNumber)
                .NotEmpty()
                .InclusiveBetween(0,999);

            RuleFor(x => x.Model.DateOfBirth)
                .NotEmpty()
                .Must(x => DateTime.UtcNow >= x.AddYears(18));

            RuleFor(m => m.Model.ClinicId)
                .NotEmpty()
                .MustAsync(async (x, ct) => await dbContext.Clinics.AsNoTracking().AnyAsync(p => p.Id == x, ct)
            );
            RuleFor(m => m.Model)
                .NotEmpty()
                .MustAsync(async (x, ct) => !await dbContext.Patients.AsNoTracking()
                    .AnyAsync(p => p.ClinicId == x.ClinicId && p.PatientNumber == x.PatientNumber, ct)
            );
        }
    }
}
