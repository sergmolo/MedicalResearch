using FluentValidation;
using MedicalResearch.Business.Commands.Users;
using MedicalResearch.Business.Models;
using MedicalResearch.Data;
using Microsoft.EntityFrameworkCore;

namespace MedicalResearch.Business.Validators
{
    public class RemoveUserValidator : AbstractValidator<RemoveUserCommand>
    {
        public RemoveUserValidator(ApplicationDbContext dbContext)
        {
            RuleFor(m => m.UserId)
                .NotEmpty()
                .MustAsync(async (id, ct) => await dbContext.Users.AsNoTracking().AnyAsync(p => p.Id == id, ct))
                .WithMessage("Wrong user ID")
                .WithState(s => new NotFound());
        }
    }
}
