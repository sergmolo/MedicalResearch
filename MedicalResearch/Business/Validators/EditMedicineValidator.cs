using FluentValidation;
using MedicalResearch.Business.Commands.Medicines;
using MedicalResearch.Business.Pipeline;
using MedicalResearch.Data;
using Microsoft.EntityFrameworkCore;

namespace MedicalResearch.Business.Validators
{
    public class EditMedicineValidator : AbstractValidator<EditMedicineCommand>
    {
        public EditMedicineValidator(ApplicationDbContext dbContext)
        {
            RuleFor(m => m.Id)
                .NotEmpty()
                .MustAsync(async (id, ct) => await dbContext.Medicines.AsNoTracking().AnyAsync(p => p.Id == id, ct))
                .WithMessage("Wrong medicine ID")
                .WithState(s => new NotFoundState());

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
