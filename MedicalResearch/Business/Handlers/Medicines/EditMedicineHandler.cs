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
            var med = await _dbContext.Medicines.FindAsync(new object[] { request.Id }, ct);

            med.Name = request.Model.Name;
            med.Description = request.Model.Description;
            med.MedicineTypeId = request.Model.MedicineTypeId;
            med.ContainerId = request.Model.ContainerId;
            med.DosageFormId = request.Model.DosageFormId;
            med.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
