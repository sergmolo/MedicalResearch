using AutoMapper;
using MediatR;
using MedicalResearch.Business.Queries.Users;
using MedicalResearch.Data;
using MedicalResearch.Data.Entities;
using MedicalResearch.V1.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Handlers.Users
{
    public class GetCurrentUserHandler : IRequestHandler<GetCurrentUserQuery, UserResponse>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetCurrentUserHandler(IMapper mapper, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserResponse> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            User user = await _userManager.FindByIdAsync(userId);
            var resp = _mapper.Map<UserResponse>(user);
            
            return resp;
        }
    }
}
