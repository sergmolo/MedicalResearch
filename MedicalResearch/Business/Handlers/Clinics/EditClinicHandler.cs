using AutoMapper;
using MediatR;
using MedicalResearch.Business.Commands.Clinics;
using MedicalResearch.Data;
using MedicalResearch.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
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
            var clinic = _mapper.Map<Clinic>(request.Model);
            clinic.Id = request.Id;
            clinic.UpdatedAt = DateTime.UtcNow;
            _dbContext.Entry(clinic).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
