using MediatR;
using MedicalResearch.Attributes;
using MedicalResearch.Business.Commands.Medicines;
using MedicalResearch.Business.Queries.Medicines;
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
    [Authorized(Role.Administrator, Role.Manager)]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MedicinesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MedicinesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task Post([FromBody] AddMedicineRequest addMedicineRequest, CancellationToken ct)
        {
            await _mediator.Send(new AddMedicineCommand(addMedicineRequest), ct);
        }

        [Authorized(Role.Administrator, Role.Manager, Role.Sponsor)]
        [HttpGet]
        public async Task<IEnumerable<MedicineResponse>> Get(CancellationToken ct,
            int pageIndex = 1, int pageSize = 10, string? sortCol = null, bool asc = true)
        {
            return await _mediator.Send(new GetAllMedicinesQuery(pageIndex, pageSize, sortCol ?? nameof(Medicine.Id), asc), ct);
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] EditMedicineRequest request, CancellationToken ct)
        {
            await _mediator.Send(new EditMedicineCommand(id, request), ct);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id, CancellationToken ct)
        {
            await _mediator.Send(new DeleteMedicineCommand(id), ct);
        }
    }
}
