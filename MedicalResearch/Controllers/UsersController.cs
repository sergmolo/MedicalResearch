using MediatR;
using MedicalResearch.Commands;
using MedicalResearch.Enums;
using MedicalResearch.Queries;
using MedicalResearch.Requests;
using MedicalResearch.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IMediator _mediator;
		
		public UsersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[AllowAnonymous]
		[HttpPost("Register")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(IEnumerable<IdentityError>), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> Register([FromBody] RegisterRequest registerRequest, CancellationToken ct)
		{
			var command = new RegisterUserCommand(registerRequest);
			var result = await _mediator.Send(command, ct);
			
			if (result.Succeeded)
			{
				return new StatusCodeResult(201);
			}

			return BadRequest(result.Errors);
		}

		[Authorized(Role.Administrator, Role.Sponsor)]
		[HttpGet("All")]
		public async Task<IEnumerable<UserResponse>> GetAll(CancellationToken ct)
		{
			return await _mediator.Send(new GetAllUsersQuery(), ct);
		}

		[Authorized(Role.Administrator, Role.Sponsor)]
		[HttpGet("{id}")]
		public async Task<UserResponse> GetById(int id, CancellationToken ct)
		{
			return await _mediator.Send(new GetUserByIdQuery(id), ct);
		}

		[HttpGet]
		public async Task<UserResponse> Get(CancellationToken ct)
		{
			return await _mediator.Send(new GetCurrentUserQuery(), ct);
		}

		[HttpPut]
		public async Task Put([FromBody] EditUserRequest editUserRequest, CancellationToken ct)
		{
			await _mediator.Send(new EditUserCommand(editUserRequest), ct);
		}

		[HttpDelete]
		public async Task<CommandResult> Remove(CancellationToken ct)
		{
			return await _mediator.Send(new RemoveUserCommand(), ct);
		}
	}
}
