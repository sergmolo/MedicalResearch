using AutoMapper;
using MediatR;
using MedicalResearch.Business.Queries.Stock;
using MedicalResearch.Data;
using MedicalResearch.V1.Responses;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Stock
{
    public class GetStockItemsHandler : IRequestHandler<GetAllStockQuery, IEnumerable<StockResponse>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetStockItemsHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StockResponse>> Handle(GetAllStockQuery request, CancellationToken ct)
        {
            var query = _dbContext.Stock.AsNoTracking()
                .Include(s => s.Medicine)
                    .ThenInclude(t => t.MedicineType)
                .Include(s => s.Medicine)
                    .ThenInclude(c => c.Container)
                .Include(s => s.Medicine)
                    .ThenInclude(d => d.DosageForm)
                .AsQueryable();

            var skip = (request.PageIndex - 1) * request.PageSize;
            query = query.Skip(skip).Take(request.PageSize);

            return await query.Select(m => _mapper.Map<StockResponse>(m)).ToListAsync(ct);
        }
    }
}
