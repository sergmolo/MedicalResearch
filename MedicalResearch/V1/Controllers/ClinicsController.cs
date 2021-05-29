using MediatR;
using MedicalResearch.Attributes;
using MedicalResearch.Business.Commands.Clinics;
using MedicalResearch.Business.Queries.Clinics;
using MedicalResearch.Data.Entities;
using MedicalResearch.Data.Enums;
using MedicalResearch.V1.Requests;
using MedicalResearch.V1.Responses;
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
            return await _mediator.Send(new GetAllClinicsQuery(pageIndex, pageSize, sortCol ?? nameof(Clinic.Id), asc), ct);
        }

        [HttpPut("{id}")]
        public async Task PutClinic(int id, EditClinicRequest clinic, CancellationToken ct)
        {
            await _mediator.Send(new EditClinicCommand(id, clinic), ct);
        }

        [HttpPost]
        public async Task PostClinic(AddClinicRequest clinicRequest, CancellationToken ct)
        {
            await _mediator.Send(new AddClinicCommand(clinicRequest), ct);
        }
    }
}
