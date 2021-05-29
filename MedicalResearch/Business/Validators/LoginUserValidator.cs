using FluentValidation;
using MedicalResearch.Business.Commands.Auth;
using MedicalResearch.Business.Models;
using MedicalResearch.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;

namespace MedicalResearch.Business.Validators
{
    public class LoginUserValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserValidator(SignInManager<User> signInManager, UserManager<User> userManager, IOptions<AuthOptions> authOptions)
        {
            RuleFor(x => x.Model).CustomAsync(async (req, context, ct) => 
            {
                var user = await userManager.FindByEmailAsync(req.Email);
                if (user is null)
                {
                    context.AddFailure("User not found");
                    return;
                }
                if (user.IsRemoved)
                {
                    context.AddFailure("User removed");
                    return;
                }
                if (await userManager.IsLockedOutAsync(user))
                {
                    context.AddFailure("User is locked out");
                    return;
                }
                if (!(await signInManager.CheckPasswordSignInAsync(user, req.Password, true)).Succeeded)
                {
                    context.AddFailure("Wrong password");
                    return;
                }
                if (user.PasswordCreatedAt.AddMonths(authOptions.Value.PasswordExpireMonth) < DateTime.UtcNow)
                {
                    context.AddFailure("Password expired");
                    return;
                }
            });
        }
    }
}
