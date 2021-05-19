using FluentValidation;
using MedicalResearch.V1.Requests;
using System.Linq;

namespace MedicalResearch.V1.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().Must(s => !s.Any(char.IsDigit));
            RuleFor(x => x.LastName).NotEmpty().Must(s => !s.Any(char.IsDigit));
            RuleFor(x => x.Initials).NotEmpty().Must(s => s.All(char.IsLetter));
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).MinimumLength(8);
        }
    }
}
