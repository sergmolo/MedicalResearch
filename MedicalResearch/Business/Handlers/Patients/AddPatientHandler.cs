using AutoMapper;
using MediatR;
using MedicalResearch.Business.Commands.Patients;
using MedicalResearch.Data;
using MedicalResearch.Data.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Patients
{
    public class AddPatientHandler : IRequestHandler<AddPatientCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddPatientHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddPatientCommand request, CancellationToken ct)
        {
            var newPatient = _mapper.Map<Patient>(request.Model);

            await _dbContext.Visits.AddAsync(new Visit()
            {
                Date = DateTime.UtcNow,
                Patient = newPatient
            }, ct);

            await _dbContext.Patients.AddAsync(newPatient, ct);
            await _dbContext.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
