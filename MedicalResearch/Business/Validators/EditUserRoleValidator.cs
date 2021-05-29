using FluentValidation;
using MedicalResearch.Business.Commands.Users;
using MedicalResearch.Business.Models;
using MedicalResearch.Data;
using MedicalResearch.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace MedicalResearch.Business.Validators
{
    public class EditUserRoleValidator : AbstractValidator<EditUserRoleCommand>
    {
        public EditUserRoleValidator(ApplicationDbContext dbContext)
        {
            RuleFor(m => m.Role)
                .NotEqual(Role.Administrator);

            RuleFor(m => m.UserId)
                .NotEmpty()
                .MustAsync(async (id, ct) => await dbContext.Users.AsNoTracking().AnyAsync(p => p.Id == id, ct))
                .WithMessage("Wrong user ID")
                .WithState(s => new NotFoundState());
        }
    }
}
