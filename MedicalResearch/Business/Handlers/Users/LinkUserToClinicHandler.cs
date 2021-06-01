using MediatR;
using MedicalResearch.Business.Commands.Users;
using MedicalResearch.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Users
{
    public class LinkUserToClinicHandler : IRequestHandler<LinkUserToClinicCommand>
    {
        private readonly ApplicationDbContext _dbContext;

        public LinkUserToClinicHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(LinkUserToClinicCommand request, CancellationToken ct)
        {
            var user = await _dbContext.Users.SingleAsync(c => c.Id == request.UserId, ct);

            user.ClinicId = request.ClinicId;
            user.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
