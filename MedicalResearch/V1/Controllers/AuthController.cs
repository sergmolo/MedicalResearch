using MediatR;
using MedicalResearch.Business.Commands.Auth;
using MedicalResearch.V1.Requests;
using Microsoft.AspNetCore.Authorization;
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
        public async Task Login([FromBody] LoginRequest loginRequest, CancellationToken ct)
        {
            await _mediator.Send(new LoginUserCommand(loginRequest), ct);
        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task Logout(CancellationToken ct)
        {
            await _mediator.Send(new LogoutUserCommand(), ct);
        }
    }
}
