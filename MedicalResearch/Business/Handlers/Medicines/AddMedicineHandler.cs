using MediatR;
using MedicalResearch.Business.Commands.Medicines;
using MedicalResearch.Business.Exceptions;
using MedicalResearch.Business.Pipeline;
using MedicalResearch.Data;
using MedicalResearch.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Medicines
{
    public class AddMedicineHandler : IRequestHandler<AddMedicineCommand>
    {
        private readonly ApplicationDbContext _dbContext;

        public AddMedicineHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(AddMedicineCommand request, CancellationToken ct)
        {
            var medType = await _dbContext.MedicineTypes.SingleAsync(t => t.Name == request.Model.MedicineType, ct);
            var container = await _dbContext.Containers.SingleAsync(t => t.Name == request.Model.Container, ct);
            var dosageForm = await _dbContext.DosageForms.SingleAsync(t => t.Name == request.Model.DosageForm, ct);

            var med = new Medicine()
            {
                Name = request.Model.Name,
                Description = request.Model.Description,
                MedicineType = medType,
                Container = container,
                DosageForm = dosageForm,
                CreatedAt = DateTime.UtcNow
            };

            await _dbContext.Medicines.AddAsync(med, ct);
            await _dbContext.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
