using MediatR;
using MedicalResearch.Business.Commands.Users;
using MedicalResearch.Business.Queries.Users;
using MedicalResearch.V1.Requests;
using MedicalResearch.V1.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MedicalResearch.Attributes;
using MedicalResearch.Business.Models;
using MedicalResearch.Data.Enums;

namespace MedicalResearch.V1.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
            var result = await _mediator.Send(new RegisterUserCommand(registerRequest), ct);

            return result.Succeeded ? new StatusCodeResult(201) : BadRequest(result.Errors);
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
