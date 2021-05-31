using AutoMapper;
using MediatR;
using MedicalResearch.Business.Commands.Medicines;
using MedicalResearch.Data;
using MedicalResearch.Data.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Medicines
{
    public class AddMedicineHandler : IRequestHandler<AddMedicineCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddMedicineHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddMedicineCommand request, CancellationToken ct)
        {
            var med = _mapper.Map<Medicine>(request.Model);

            await _dbContext.Medicines.AddAsync(med, ct);
            await _dbContext.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
