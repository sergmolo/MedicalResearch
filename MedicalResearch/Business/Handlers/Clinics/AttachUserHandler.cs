using AutoMapper;
using MediatR;
using MedicalResearch.Business.Commands.Clinics;
using MedicalResearch.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Clinics
{
    public class AttachUserHandler : IRequestHandler<AttachUserCommand>
    {
        private readonly ApplicationDbContext _dbContext;

        public AttachUserHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(AttachUserCommand request, CancellationToken ct)
        {
            var user = await _dbContext.Users.SingleAsync(c => c.Id == request.UserId, ct);

            user.ClinicId = request.ClinicId;

            await _dbContext.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
