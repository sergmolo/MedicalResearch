using FluentValidation;
using MedicalResearch.Business.Commands.Medicines;
using MedicalResearch.Data;
using Microsoft.EntityFrameworkCore;

namespace MedicalResearch.Business.Validators
{
    public class AddMedicineValidator : AbstractValidator<AddMedicineCommand>
    {
        public AddMedicineValidator(ApplicationDbContext dbContext)
        {
            RuleFor(m => m.Model.Name)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(m => m.Model.Description)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(m => m.Model.MedicineType)
                .NotEmpty()
                .MustAsync(async (name, ct) =>
                            await dbContext.MedicineTypes.AsNoTracking().AnyAsync(p => p.Name == name, ct));

            RuleFor(m => m.Model.Container)
                .NotEmpty()
                .MustAsync(async (name, ct) =>
                            await dbContext.Containers.AsNoTracking().AnyAsync(p => p.Name == name, ct));

            RuleFor(m => m.Model.DosageForm)
                .NotEmpty()
                .MustAsync(async (name, ct) =>
                            await dbContext.DosageForms.AsNoTracking().AnyAsync(p => p.Name == name, ct));
        }
    }
}
