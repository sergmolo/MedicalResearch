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
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync(async (id, ct) => await dbContext.MedicineTypes.AsNoTracking().AnyAsync(p => p.Id == id, ct));
                
            RuleFor(m => m.Model.ContainerId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync(async (id, ct) => await dbContext.Containers.AsNoTracking().AnyAsync(p => p.Id == id, ct));
                
            RuleFor(m => m.Model.DosageFormId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync(async (id, ct) => await dbContext.DosageForms.AsNoTracking().AnyAsync(p => p.Id == id, ct));
                
            RuleFor(m => m.Model)
                .MustAsync(async (req, ct) => !await dbContext.Medicines.AsNoTracking()
                    .AnyAsync(p => p.Name.ToLower() == req.Name.ToLower() && p.MedicineTypeId == req.MedicineTypeId
                        && req.DosageFormId == p.DosageFormId && req.ContainerId == p.ContainerId, ct))
                .WithMessage("This medicine already exists");
        }
    }
}
