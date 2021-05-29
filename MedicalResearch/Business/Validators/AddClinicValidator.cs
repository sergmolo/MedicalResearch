using FluentValidation;
using MedicalResearch.Business.Commands.Clinics;
using System.Linq;

namespace MedicalResearch.Business.Validators
{
    public class AddClinicValidator : AbstractValidator<AddClinicCommand>
    {
        public AddClinicValidator()
        {
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
