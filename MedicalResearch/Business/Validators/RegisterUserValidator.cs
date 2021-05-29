using FluentValidation;
using MedicalResearch.Business.Commands.Users;
using System.Linq;

namespace MedicalResearch.Business.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Model.FirstName)
                .NotEmpty()
                .Must(s => !s.Any(char.IsDigit));

            RuleFor(x => x.Model.LastName)
                .NotEmpty()
                .Must(s => !s.Any(char.IsDigit));

            RuleFor(x => x.Model.Initials)
                .NotEmpty()
                .Must(s => s.All(char.IsLetter));

            RuleFor(x => x.Model.Email)
                .EmailAddress();

            RuleFor(x => x.Model.Password)
                .MinimumLength(8);
        }
    }
}
