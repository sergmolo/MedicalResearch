using AutoMapper;
using MediatR;
using MedicalResearch.Business.Queries.Patients;
using MedicalResearch.Data;
using MedicalResearch.V1.Responses;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Patients
{
    public class GetAllPatientsHandler : IRequestHandler<GetAllPatientsQuery, IEnumerable<PatientResponse>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllPatientsHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PatientResponse>> Handle(GetAllPatientsQuery request, CancellationToken ct)
        {
            var query = _dbContext.Patients.Include(p => p.Visits).ThenInclude(v => v.Medicine).AsNoTracking().AsQueryable();

            if(request.ClinicId is not null)
            {
                query = query.Where(x => x.ClinicId == request.ClinicId);
            }

            var skip = (request.PageIndex - 1) * request.PageSize;
            query = query.Skip(skip).Take(request.PageSize);

            var q = query.Select(m => _mapper.Map<PatientResponse>(m));

            return await q.ToListAsync(ct);
        }
    }
}
