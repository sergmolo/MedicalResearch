using AutoMapper;
using MediatR;
using MedicalResearch.Business.Commands.Clinics;
using MedicalResearch.Data;
using MedicalResearch.Data.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Clinics
{
    public class AddClinicHandler : IRequestHandler<AddClinicCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddClinicHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddClinicCommand request, CancellationToken ct)
        {
            var clinic = _mapper.Map<Clinic>(request.Model);

            await _dbContext.Clinics.AddAsync(clinic, ct);
            await _dbContext.SaveChangesAsync(ct);
            
            return Unit.Value;
        }
    }
}
