using FluentValidation;
using MedicalResearch.Business.Models;
using MedicalResearch.Business.Queries.Users;
using MedicalResearch.Data;
using Microsoft.EntityFrameworkCore;

namespace MedicalResearch.Business.Validators
{
    public class GetUserByIdValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .MustAsync(async (x, ct) => await dbContext.Users.AnyAsync(e => e.Id == x, ct))
                .WithMessage("Wrong user ID")
                .WithState(s => new NotFoundState());
        }
    }
}
