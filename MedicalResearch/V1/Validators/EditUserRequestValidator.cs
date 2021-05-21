using FluentValidation;
using MedicalResearch.V1.Requests;
using System.Linq;

namespace MedicalResearch.V1.Validators
{
    public class EditUserRequestValidator : AbstractValidator<EditUserRequest>
    {
        public EditUserRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().Must(s => !s.Any(char.IsDigit));
            RuleFor(x => x.LastName).NotEmpty().Must(s => !s.Any(char.IsDigit));
            RuleFor(x => x.Initials).NotEmpty().Must(s => s.All(char.IsLetter));
            RuleFor(x => x.NewPassword).MinimumLength(8);
        }
    }
}
