using FluentValidation;
using MedicalResearch.Business.Commands.Users;
using MedicalResearch.Business.Models;
using MedicalResearch.Data;
using MedicalResearch.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace MedicalResearch.Business.Validators
{
    public class LinkUserToClinicValidator : AbstractValidator<LinkUserToClinicCommand>
    {
        public LinkUserToClinicValidator(ApplicationDbContext dbContext)
        {
            RuleFor(m => m.ClinicId)
                .NotEmpty()
                .MustAsync(async (id, ct) => await dbContext.Clinics.AsNoTracking().AnyAsync(p => p.Id == id, ct))
                .WithMessage("Wrong clinic ID");

            RuleFor(m => m.UserId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync(async (id, ct) => await dbContext.Users.AsNoTracking().AnyAsync(p => p.Id == id, ct))
                .WithMessage("Wrong user ID")
                .WithState(s => new NotFound())
                .MustAsync(async (id, ct) =>
                {
                    var user = await dbContext.Users.AsNoTracking().SingleAsync(p => p.Id == id, ct);

                    return user.Role == Role.Researcher;
                })
                .WithMessage("User role is not researcher");
        }
    }
}
