using MediatR;
using MedicalResearch.Attributes;
using MedicalResearch.Business.Commands.Users;
using MedicalResearch.Business.Queries.Users;
using MedicalResearch.Data.Enums;
using MedicalResearch.V1.Requests;
using MedicalResearch.V1.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<UserResponse> Get(CancellationToken ct)
        {
            return await _mediator.Send(new GetUserByIdQuery(GetCurrentUserId()), ct);
        }

        [Authorized(Role.Administrator, Role.Sponsor)]
        [HttpGet("{id}")]
        public async Task<UserResponse> GetById(int id, CancellationToken ct)
        {
            return await _mediator.Send(new GetUserByIdQuery(id), ct);
        }

        [Authorized(Role.Administrator, Role.Sponsor)]
        [HttpGet("All")]
        public async Task<IEnumerable<UserResponse>> GetAll(CancellationToken ct)
        {
            return await _mediator.Send(new GetAllUsersQuery(), ct);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task Register([FromBody] RegisterRequest registerRequest, CancellationToken ct)
        {
            await _mediator.Send(new RegisterUserCommand(registerRequest), ct);
        }

        [HttpPost("{id}/LinkToClinic")]
        public async Task LinkToClinic(int id, [FromBody] LinkUserToClinicRequest request, CancellationToken ct)
        {
            await _mediator.Send(new LinkUserToClinicCommand(id, request), ct);
        }

        [Authorized(Role.Administrator, Role.Sponsor)]
        [HttpPut("{id}/EditRole")]
        public async Task EditUserRole(int id, [FromBody] EditUserRoleRequest request, CancellationToken ct)
        {
            await _mediator.Send(new EditUserRoleCommand(id, request), ct);
        }

        [HttpPut]
        public async Task Put([FromBody] EditUserRequest editUserRequest, CancellationToken ct)
        {
            await _mediator.Send(new EditUserCommand(GetCurrentUserId(), editUserRequest), ct);
        }

        [HttpDelete]
        public async Task Remove(CancellationToken ct)
        {
            await _mediator.Send(new RemoveUserCommand(GetCurrentUserId()), ct);
        }

        private int GetCurrentUserId()
        {
            return int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
    }
}
