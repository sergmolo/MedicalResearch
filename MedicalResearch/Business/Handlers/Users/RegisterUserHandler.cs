using AutoMapper;
using MediatR;
using MedicalResearch.Business.Commands.Users;
using MedicalResearch.Business.Models;
using MedicalResearch.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Users
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public RegisterUserHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.Model);

            var res = await _userManager.CreateAsync(user, request.Model.Password);

            if (!res.Succeeded) throw new BusinessLogicException(CommandResult.Failed(res.Errors));

            return Unit.Value;
        }
    }
}
