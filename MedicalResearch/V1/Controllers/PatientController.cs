using MediatR;
using MedicalResearch.Attributes;
using MedicalResearch.Business.Commands.Patients;
using MedicalResearch.Business.Queries.Patients;
using MedicalResearch.Business.Queries.Users;
using MedicalResearch.Data.Enums;
using MedicalResearch.V1.Requests;
using MedicalResearch.V1.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.V1.Controllers
{
    [Authorized(Role.Researcher, Role.Administrator)]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{clinicId}-{patientNumber}")]
        public async Task<ActionResult<PatientInfoResponse>> GetPatient(int clinicId, int patientNumber, CancellationToken ct)
        {
            var currrentClinicId = await GetCurrentUserClinicId();
            if (currrentClinicId.HasValue && currrentClinicId.Value == clinicId)
            {
                return await _mediator.Send(new GetPatientQuery(new PatientRequest() { ClinicId = clinicId, PatientNumber = patientNumber }), ct);
            }
            else
            {
                return Forbid();
            }
        }

        [Authorized(Role.Researcher, Role.Administrator, Role.Sponsor)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientResponse>>> GetPatients(CancellationToken ct, int pageIndex = 1, int pageSize = 10)
        {
            int? clinicId;
            var user = HttpContext.User;
            if (user.IsInRole(Role.Sponsor.ToString()) || user.IsInRole(Role.Administrator.ToString()))
            {
                clinicId = null;
            }
            else
            {
                clinicId = await GetCurrentUserClinicId();
                if (clinicId is null) return Forbid();
            }

            return Ok(await _mediator.Send(new GetAllPatientsQuery(pageIndex, pageSize, clinicId), ct));
        }

        [HttpPost]
        public async Task<IActionResult> PostPatient(AddPatientRequest patientRequest, CancellationToken ct)
        {
            var clinicId = await GetCurrentUserClinicId();
            if (clinicId.HasValue && clinicId.Value == patientRequest.ClinicId)
            {
                await _mediator.Send(new AddPatientCommand(patientRequest), ct);
                return Ok();
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost("PerformVisit")]
        public async Task<IActionResult> PerformVisit(PatientRequest request, CancellationToken ct)
        {
            var clinicId = await GetCurrentUserClinicId();
            if (clinicId.HasValue && clinicId.Value == request.ClinicId)
            {
                await _mediator.Send(new PerformVisitCommand(request), ct);
                return Ok();
            }
            else
            {
                return Forbid();
            }
        }

        private async Task<int?> GetCurrentUserClinicId()
        {
            var userId = GetCurrentUserId();
            var user = await _mediator.Send(new GetUserByIdQuery(userId));
            return user.ClinicId;
        }

        private int GetCurrentUserId()
        {
            return int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
    }
}
