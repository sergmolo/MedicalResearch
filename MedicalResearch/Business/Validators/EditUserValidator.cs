using FluentValidation;
using MedicalResearch.Business.Commands.Users;
using MedicalResearch.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MedicalResearch.Business.Validators
{
    public class EditUserValidator : AbstractValidator<EditUserByIdCommand>
    {
        public EditUserValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Model.FirstName).NotEmpty().Must(s => !s.Any(char.IsDigit));
            RuleFor(x => x.Model.LastName).NotEmpty().Must(s => !s.Any(char.IsDigit));
            RuleFor(x => x.Model.Initials).NotEmpty().Must(s => s.All(char.IsLetter));
            RuleFor(x => x.Model.NewPassword).MinimumLength(8);
            RuleFor(m => m.UserId).NotEmpty().MustAsync(async (id, ct) =>
                                    await dbContext.Users.AsNoTracking().AnyAsync(p => p.Id == id, ct));
        }
    }
}
