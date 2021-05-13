using AutoMapper;
using MediatR;
using MedicalResearch.Enums;
using MedicalResearch.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Commands.Users
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
			user.UserName = request.Model.Email;
			user.IsRemoved = false;
			user.CreatedAt = DateTime.UtcNow;
			user.PasswordCreatedAt = DateTime.UtcNow;
			user.RoleId = (int)Role.Researcher;

			return await _userManager.CreateAsync(user, request.Model.Password);
		}
	}
}
