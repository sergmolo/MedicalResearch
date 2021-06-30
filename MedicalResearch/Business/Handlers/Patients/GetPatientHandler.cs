using AutoMapper;
using MediatR;
using MedicalResearch.Business.Queries.Patients;
using MedicalResearch.Data;
using MedicalResearch.V1.Responses;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Patients
{
    public class GetPatientHandler : IRequestHandler<GetPatientQuery, PatientInfoResponse>
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public GetPatientHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PatientInfoResponse> Handle(GetPatientQuery request, CancellationToken ct)
        {
            var patient = await _dbContext.Patients.AsNoTracking().Include(p => p.Visits)
                .FirstOrDefaultAsync(u => u.ClinicId == request.PatientRequest.ClinicId
                        && u.PatientNumber == request.PatientRequest.PatientNumber, ct);

            return _mapper.Map<PatientInfoResponse>(patient);
        }
    }
}
