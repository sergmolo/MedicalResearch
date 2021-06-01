using MediatR;
using MedicalResearch.Business.Commands.Medicines;
using MedicalResearch.Data;
using MedicalResearch.Data.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Medicines
{
    public class DeleteMedicineHandler : IRequestHandler<DeleteMedicineCommand>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DeleteMedicineHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Unit> Handle(DeleteMedicineCommand request, CancellationToken cancellationToken)
        {
            _applicationDbContext.Medicines.Remove(new Medicine() { Id = request.Id });
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
