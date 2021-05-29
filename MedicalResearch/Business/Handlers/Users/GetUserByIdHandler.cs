using AutoMapper;
using MediatR;
using MedicalResearch.Business.Queries.Users;
using MedicalResearch.Data;
using MedicalResearch.V1.Responses;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Users
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public GetUserByIdHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.AsNoTracking().SingleAsync(u => u.Id == request.Id, cancellationToken);

            return _mapper.Map<UserResponse>(user);
        }
    }
}
