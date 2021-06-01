using AutoMapper;
using MediatR;
using MedicalResearch.Business.Commands.Clinics;
using MedicalResearch.Data;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Clinics
{
    public class EditClinicHandler : IRequestHandler<EditClinicCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public EditClinicHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(EditClinicCommand request, CancellationToken ct)
        {
            var clinic = await _dbContext.Clinics.FindAsync(new object[] { request.Id }, ct);

            _mapper.Map(request.Model, clinic);

            await _dbContext.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
