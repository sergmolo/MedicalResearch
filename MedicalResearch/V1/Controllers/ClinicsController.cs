using MediatR;
using MedicalResearch.Attributes;
using MedicalResearch.Business.Commands.Clinics;
using MedicalResearch.Business.Queries.Clinics;
using MedicalResearch.Data.Enums;
using MedicalResearch.V1.Requests;
using MedicalResearch.V1.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.V1.Controllers
{
    [Authorized(Role.Sponsor, Role.Administrator)]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ClinicsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClinicsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<ClinicResponse>> GetClinics(CancellationToken ct,
            int pageIndex = 1, int pageSize = 10, string? sortCol = null, bool asc = true)
        {
            return await _mediator.Send(new GetAllClinicsQuery(pageIndex, pageSize, sortCol ?? "id", asc), ct);
        }

        [HttpPut("{id}")]
        public async Task PutClinic(int id, ClinicRequest clinic, CancellationToken ct)
        {
            await _mediator.Send(new EditClinicCommand(id, clinic), ct);
        }

        [HttpPost]
        public async Task<ActionResult> PostClinic(ClinicRequest clinicRequest, CancellationToken ct)
        {
            await _mediator.Send(new AddClinicCommand(clinicRequest), ct);
            return new StatusCodeResult(StatusCodes.Status201Created);
        }

        [HttpPost("{id}/AttachUser")]
        public async Task AttachUser(int id, [FromBody] AttachUserRequest request, CancellationToken ct)
        {
            await _mediator.Send(new AttachUserCommand(id, request), ct);
        }
    }
}
