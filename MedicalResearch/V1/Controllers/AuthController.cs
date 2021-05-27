using MediatR;
using MedicalResearch.Business.Commands.Auth;
using MedicalResearch.Business.Models;
using MedicalResearch.V1.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CommandResult>> Login([FromBody] LoginRequest loginRequest, CancellationToken ct)
        {
            var result = await _mediator.Send(new LoginUserCommand(loginRequest), ct);
            return result.Succeeded ? Ok() : BadRequest(result);
        }

        [Authorize]
        [HttpPost("Logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout(CancellationToken ct)
        {
            await _mediator.Send(new LogoutUserCommand(), ct);
            return Ok();
        }
    }
}
