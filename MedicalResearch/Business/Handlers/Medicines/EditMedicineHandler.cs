using MediatR;
using MedicalResearch.Business.Commands.Medicines;
using MedicalResearch.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Medicines
{
    public class EditMedicineHandler : IRequestHandler<EditMedicineCommand>
    {
        private readonly ApplicationDbContext _dbContext;

        public EditMedicineHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(EditMedicineCommand request, CancellationToken ct)
        {
            var medType = await _dbContext.MedicineTypes.SingleAsync(t => t.Name == request.Model.MedicineType, ct);
            var container = await _dbContext.Containers.SingleAsync(t => t.Name == request.Model.Container, ct);
            var dosageForm = await _dbContext.DosageForms.SingleAsync(t => t.Name == request.Model.DosageForm, ct);
            
            var med = await _dbContext.Medicines.FindAsync(request.Id, ct);

            med.Name = request.Model.Name;
            med.Description = request.Model.Description;
            med.MedicineType = medType;
            med.Container = container;
            med.DosageForm = dosageForm;
            med.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
