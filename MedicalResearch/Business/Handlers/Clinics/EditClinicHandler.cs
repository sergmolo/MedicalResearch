using MediatR;
using MedicalResearch.Business.Commands.Clinics;
using MedicalResearch.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Clinics
{
    public class EditClinicHandler : IRequestHandler<EditClinicCommand>
    {
        private readonly ApplicationDbContext _dbContext;

        public EditClinicHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(EditClinicCommand request, CancellationToken ct)
        {
            var clinic = await _dbContext.Clinics.FindAsync(new object[] { request.Id }, ct);

            clinic.Id = request.Id;
            clinic.Name = request.Model.Name;
            clinic.City = request.Model.City;
            clinic.Phone = request.Model.Phone;
            clinic.Address = request.Model.Address;
            clinic.Address2 = request.Model.Address2;
            clinic.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
