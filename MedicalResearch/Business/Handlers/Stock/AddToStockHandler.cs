using AutoMapper;
using MediatR;
using MedicalResearch.Business.Commands.Stock;
using MedicalResearch.Data;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Stock
{
    public class AddToStockHandler : IRequestHandler<AddToStockCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddToStockHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddToStockCommand request, CancellationToken ct)
        {
            var stock = _mapper.Map<Data.Entities.Stock>(request.Model);
            await _dbContext.Stock.AddAsync(stock, ct);
            await _dbContext.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
