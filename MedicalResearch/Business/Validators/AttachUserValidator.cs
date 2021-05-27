using FluentValidation;
using MedicalResearch.Business.Commands.Clinics;
using MedicalResearch.Data;
using Microsoft.EntityFrameworkCore;

namespace MedicalResearch.Business.Validators
{
    public class AttachUserValidator : AbstractValidator<AttachUserCommand>
    {
        public AttachUserValidator(ApplicationDbContext dbContext)
        {
            RuleFor(m => m.ClinicId).NotEmpty().MustAsync(async (id, ct) =>
                                    await dbContext.Clinics.AsNoTracking().AnyAsync(p => p.Id == id, ct))
                .WithMessage("WrongClinicId");

            RuleFor(m => m.UserId).NotEmpty().MustAsync(async (id, ct) => 
                await dbContext.Users.AsNoTracking().AnyAsync(p => p.Id == id, ct))
                .WithMessage("WrongUserId");

            RuleFor(m => m.UserId).NotEmpty().MustAsync(async (id, ct) =>
                (await dbContext.Users.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id, ct))?.Role == Data.Enums.Role.Researcher)
                .WithMessage("UserRoleIsNotResearcher");
        }
    }
}
