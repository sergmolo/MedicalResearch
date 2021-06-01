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
    public class GetAllDosageFormsHandler : IRequestHandler<GetAllDosageFormsQuery, IEnumerable<DosageFormResponse>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllDosageFormsHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DosageFormResponse>> Handle(GetAllDosageFormsQuery request, CancellationToken ct)
        {
            var query = _dbContext.DosageForms.AsNoTracking()
                .OrderBy(p => p.Name)
                .Select(m => _mapper.Map<DosageFormResponse>(m));

            return await query.ToListAsync(ct);
        }
    }
}
