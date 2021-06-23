using FluentValidation;
using MedicalResearch.Business.Commands.Stock;
using MedicalResearch.Data;
using MedicalResearch.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace MedicalResearch.Business.Validators
{
    public class SupplyValidator : AbstractValidator<SupplyCommand>
    {
        public SupplyValidator(ApplicationDbContext dbContext)
        {
            RuleForEach(x => x.Models)
                .Must(x => x.Amount > 0)
                .WithMessage("Amount must be positive"); ;

            RuleForEach(x => x.Models)
                .MustAsync(async (x, ct) => await dbContext.Clinics.AsNoTracking()
                    .AnyAsync(m => m.Id == x.ClinicId, ct))
                .WithMessage("Wrong clinic ID");
            
            RuleForEach(x => x.Models).MustAsync(async (x, ct) => await dbContext.Stock.AsNoTracking()
                    .AnyAsync(m => m.MedicineId == x.MedicineId && m.ExpireAt > DateTime.UtcNow && m.MedicineState == MedicineState.Ok, ct))
                .WithMessage("Medicine was not found in stock");

            RuleFor(x => x.Models).MustAsync(async (x, ct) =>
            {
                var requestMedicines = x.GroupBy(x => x.MedicineId)
                .Select(group => new
                {
                    MedicineId = group.Key,
                    Count = group.Sum(x => x.Amount)
                })
                .ToList();

                var stockMedicines = await dbContext.Stock.AsNoTracking()
                .Where(m => m.Amount != 0 && m.ExpireAt > DateTime.UtcNow && m.MedicineState == MedicineState.Ok)
                .GroupBy(x => x.MedicineId)
                .Select(group => new
                {
                    MedicineId = group.Key,
                    Count = group.Sum(x => x.Amount)
                })
                .ToListAsync(ct);

                var res = requestMedicines.Join(stockMedicines, x => x.MedicineId, y => y.MedicineId,
                    (x, y) => x.Count <= y.Count).All(x => x == true);

                return res;
            })
            .WithMessage("Not enough medicines in stock");
        }
    }
}
