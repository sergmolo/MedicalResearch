using MediatR;
using MedicalResearch.Business.Commands.Stock;
using MedicalResearch.Data;
using MedicalResearch.Data.Entities;
using MedicalResearch.Data.Enums;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Stock
{
    public class SupplyHandler : IRequestHandler<SupplyCommand>
    {
        private readonly ApplicationDbContext _dbContext;

        public SupplyHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(SupplyCommand request, CancellationToken ct)
        {
            var stockItems = _dbContext.Stock.Where(s => s.Amount != 0 
                && s.ExpireAt > DateTime.UtcNow 
                && s.MedicineState == MedicineState.Ok);

            var stockItemsByMedicineId = request.Models
                .GroupBy(m => m.MedicineId)
                .Select(gr => gr.Key)
                .GroupJoin(stockItems, medicineId => medicineId, item => item.MedicineId, 
                    (medicineId, item) => item.OrderBy(i => i.ExpireAt))
                .ToList();

            foreach (var supplyRequest in request.Models)
            {
                var requestAmount = supplyRequest.Amount;

                foreach (var stockGroup in stockItemsByMedicineId.Where(x => x.First().MedicineId == supplyRequest.MedicineId).ToList())
                {
                    foreach (var stock in stockGroup)
                    {
                        if (stock.Amount == 0) continue;

                        var amount = requestAmount <= stock.Amount ? requestAmount : stock.Amount;

                        requestAmount -= amount;
                        stock.Amount -= amount;

                        var supply = new Supply()
                        {
                            MedicineState = stock.MedicineState,
                            ExpireAt = stock.ExpireAt,
                            Amount = amount,
                            CreatedAt = DateTime.UtcNow,
                            ClinicId = supplyRequest.ClinicId,
                            MedicineId = stock.MedicineId
                        };

                        await _dbContext.Supply.AddAsync(supply, ct);

                        if (requestAmount == 0) break;
                    }
                }
            }

            await _dbContext.SaveChangesAsync(ct);

            return Unit.Value;
        }
}
}
