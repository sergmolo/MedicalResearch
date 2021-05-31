using FluentValidation;
using MedicalResearch.Business.Commands.Medicines;
using MedicalResearch.Business.Pipeline;
using MedicalResearch.Data;
using Microsoft.EntityFrameworkCore;

namespace MedicalResearch.Business.Validators
{
    public class DeleteMedicineValidator : AbstractValidator<DeleteMedicineCommand>
    {
        public DeleteMedicineValidator(ApplicationDbContext dbContext)
        {
            RuleFor(m => m.Id)
                .NotEmpty()
                .MustAsync(async (id, ct) => await dbContext.Medicines.AsNoTracking().AnyAsync(p => p.Id == id, ct))
                .WithMessage("Wrong medicine ID")
                .WithState(s => new NotFoundState());
        }
    }
}
