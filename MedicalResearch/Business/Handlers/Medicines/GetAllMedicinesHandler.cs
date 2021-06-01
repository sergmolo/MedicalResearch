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
    public class GetAllMedicinesHandler : IRequestHandler<GetAllMedicinesQuery, IEnumerable<MedicineResponse>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllMedicinesHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MedicineResponse>> Handle(GetAllMedicinesQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Medicines.AsNoTracking()
                .Include(t => t.MedicineType).Include(c => c.Container).Include(d => d.DosageForm).AsQueryable();

            query = query.OrderBy(request.SortCol, request.Asc);

            var skip = (request.PageIndex - 1) * request.PageSize;
            query = query.Skip(skip).Take(request.PageSize);

            return await query.Select(m => _mapper.Map<MedicineResponse>(m)).ToListAsync(cancellationToken);
        }
    }
}
