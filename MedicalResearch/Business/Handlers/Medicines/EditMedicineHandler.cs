using AutoMapper;
using MediatR;
using MedicalResearch.Business.Commands.Medicines;
using MedicalResearch.Data;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Medicines
{
    public class EditMedicineHandler : IRequestHandler<EditMedicineCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public EditMedicineHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(EditMedicineCommand request, CancellationToken ct)
        {
            var medicine = await _dbContext.Medicines.FindAsync(new object[] { request.Id }, ct);

            _mapper.Map(request.Model, medicine);

            await _dbContext.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
