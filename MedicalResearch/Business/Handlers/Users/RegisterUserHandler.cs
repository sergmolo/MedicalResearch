using AutoMapper;
using MediatR;
using MedicalResearch.Business.Commands.Users;
using MedicalResearch.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Users
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, IdentityResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public RegisterUserHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.Model);

            return await _userManager.CreateAsync(user, request.Model.Password);
        }
    }
}
