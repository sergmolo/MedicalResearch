using MediatR;
using MedicalResearch.Attributes;
using MedicalResearch.Business.Commands.Users;
using MedicalResearch.Business.Models;
using MedicalResearch.Business.Queries.Users;
using MedicalResearch.Data.Enums;
using MedicalResearch.V1.Requests;
using MedicalResearch.V1.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

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
        public async Task<ActionResult<UserResponse>> GetById(int id, CancellationToken ct)
        {
            var res = await _mediator.Send(new GetUserByIdQuery(id), ct);
            return res != null ? Ok(res) : BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<UserResponse>> Get(CancellationToken ct)
        {
            var res = await _mediator.Send(new GetUserByIdQuery(GetCurrentUserId()), ct);
            return res != null ? Ok(res) : BadRequest();
        }

        [HttpPut]
        public async Task Put([FromBody] EditUserRequest editUserRequest, CancellationToken ct)
        {
            await _mediator.Send(new EditUserCommand(GetCurrentUserId(), editUserRequest), ct);
        }

        [HttpDelete]
        public async Task<CommandResult> Remove(CancellationToken ct)
        {
            return await _mediator.Send(new RemoveUserByIdCommand(GetCurrentUserId()), ct);
        }

        private int GetCurrentUserId()
        {
            return int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
    }
}
