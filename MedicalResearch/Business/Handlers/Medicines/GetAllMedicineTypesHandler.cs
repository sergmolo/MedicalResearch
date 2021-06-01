using AutoMapper;
using MediatR;
using MedicalResearch.Business.Queries.Medicines;
using MedicalResearch.Data;
using MedicalResearch.Extensions;
using MedicalResearch.V1.Responses;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Medicines
{
    public class GetAllMedicineTypesHandler : IRequestHandler<GetAllMedicineTypesQuery, IEnumerable<MedicineTypeResponse>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllMedicineTypesHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MedicineTypeResponse>> Handle(GetAllMedicineTypesQuery request, CancellationToken ct)
        {
            var query = _dbContext.MedicineTypes.AsNoTracking()
                .OrderBy(p => p.Name)
                .Select(m => _mapper.Map<MedicineTypeResponse>(m));

            return await query.ToListAsync(ct);
        }
    }
}
