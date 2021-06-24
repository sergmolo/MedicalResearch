using FluentValidation;
using MedicalResearch.Business.Commands.Stock;
using MedicalResearch.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace MedicalResearch.Business.Validators
{
    public class AddToStockValidator : AbstractValidator<AddToStockCommand>
    {
        public AddToStockValidator(ApplicationDbContext dbContext)
        {
            RuleFor(m => m.Model.Amount)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(m => m.Model.ExpireAt)
                .NotEmpty()
                .GreaterThan(DateTime.UtcNow);

            RuleFor(m => m.Model.MedicineId)
                .NotEmpty()
                .MustAsync(async (x, ct) => await dbContext.Medicines.AsNoTracking().AnyAsync(t => t.Id == x, ct));
        }
    }
}
