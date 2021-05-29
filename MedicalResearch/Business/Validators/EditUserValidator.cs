using FluentValidation;
using MedicalResearch.Business.Commands.Users;
using MedicalResearch.Business.Models;
using MedicalResearch.Data;
using MedicalResearch.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MedicalResearch.Business.Validators
{
    public class EditUserValidator : AbstractValidator<EditUserCommand>
    {
        public EditUserValidator(ApplicationDbContext dbContext, UserManager<User> userManager, IPasswordValidator<User> passwordValidator)
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

            RuleFor(m => m.UserId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync(async (id, ct) => await dbContext.Users.AsNoTracking().AnyAsync(p => p.Id == id, ct))
                .WithMessage("Wrong user ID")
                .WithState(s => new NotFound())
                .DependentRules(() =>
                {
                    When(x => x.Model.NewPassword != null, () =>
                    {
                        RuleFor(x => x)
                            .MustAsync(async (req, ct) =>
                                (await passwordValidator.ValidateAsync(
                                    userManager, 
                                    await userManager.FindByIdAsync(req.UserId.ToString()), 
                                    req.Model.NewPassword)
                                ).Succeeded)
                            .WithMessage("Password is not valid");
                    });
                });
        }
    }
}
