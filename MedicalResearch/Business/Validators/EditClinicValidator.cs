using FluentValidation;
using MedicalResearch.Business.Commands.Clinics;
using MedicalResearch.Business.Models;
using MedicalResearch.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MedicalResearch.Business.Validators
{
    public class EditClinicValidator : AbstractValidator<EditClinicCommand>
    {
        public EditClinicValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .MustAsync(async (x, ct) => await dbContext.Clinics.AnyAsync(e => e.Id == x, ct))
                .WithMessage("Wrong clinic ID")
                .WithState(s => new NotFound());

            RuleFor(x => x.Model.Name)
                .NotEmpty()
                .Must(s => s.Any(char.IsLetter));

            RuleFor(x => x.Model.City)
                .NotEmpty()
                .Must(s => s.Any(char.IsLetter))
                .MinimumLength(3);

            RuleFor(x => x.Model.Phone)
                .NotEmpty()
                .Must(s => s.Any(char.IsDigit))
                .MinimumLength(5);

            RuleFor(x => x.Model.Address)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(x => x.Model.Address2)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
