using AutoMapper;
using MediatR;
using MedicalResearch.Business.Queries.Clinics;
using MedicalResearch.Data;
using MedicalResearch.Extensions;
using MedicalResearch.V1.Responses;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Clinics
{
    public class GetAllClinicsHandler : IRequestHandler<GetAllClinicsQuery, IEnumerable<ClinicResponse>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllClinicsHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClinicResponse>> Handle(GetAllClinicsQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Clinics.AsNoTracking().AsQueryable();

            query = query.OrderByPropertyName(request.SortCol, request.Asc);

            var skip = (request.PageIndex - 1) * request.PageSize;
            query = query.Skip(skip).Take(request.PageSize);

            return await query.Select(m => _mapper.Map<ClinicResponse>(m)).ToListAsync(cancellationToken);
        }
    }
}
