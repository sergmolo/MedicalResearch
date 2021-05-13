using FluentValidation;
using MedicalResearch.Requests;
using System.Linq;

namespace MedicalResearch
{
	public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
	{
		public RegisterRequestValidator()
		{
			RuleFor(x => x.FirstName).NotEmpty().Must(s => !s.Any(c => char.IsDigit(c)));
			RuleFor(x => x.LastName).NotEmpty().Must(s => !s.Any(c => char.IsDigit(c)));
			RuleFor(x => x.Initials).NotEmpty().Must(s => s.All(c => char.IsLetter(c)));
			RuleFor(x => x.Email).EmailAddress();
			RuleFor(x => x.Password).MinimumLength(8);
		}
	}
}
