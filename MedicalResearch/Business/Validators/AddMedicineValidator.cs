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

            RuleFor(m => m.Model.MedicineTypeId)
                .NotEmpty()
                .MustAsync(async (id, ct) => await dbContext.MedicineTypes.AsNoTracking().AnyAsync(p => p.Id == id, ct));

            RuleFor(m => m.Model.ContainerId)
                .NotEmpty()
                .MustAsync(async (id, ct) => await dbContext.Containers.AsNoTracking().AnyAsync(p => p.Id == id, ct));

            RuleFor(m => m.Model.DosageFormId)
                .NotEmpty()
                .MustAsync(async (id, ct) => await dbContext.DosageForms.AsNoTracking().AnyAsync(p => p.Id == id, ct));
        }
    }
}
