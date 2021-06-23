using MediatR;
using MedicalResearch.Attributes;
using MedicalResearch.Business.Commands.Stock;
using MedicalResearch.Business.Queries.Stock;
using MedicalResearch.Data.Enums;
using MedicalResearch.V1.Requests;
using MedicalResearch.V1.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.V1.Controllers
{
    [Authorized(Role.Manager, Role.Administrator)]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task Post([FromBody] AddToStockRequest addStockRequest, CancellationToken ct)
        {
            await _mediator.Send(new AddToStockCommand(addStockRequest), ct);
        }

        [HttpGet]
        public async Task<IEnumerable<StockResponse>> Get(CancellationToken ct, int pageIndex = 1, int pageSize = 10)
        {
            return await _mediator.Send(new GetAllStockQuery(pageIndex, pageSize), ct);
        }

        [HttpPost("Supply")]
        public async Task Supply([FromBody] IEnumerable<SupplyRequest> supplyRequest, CancellationToken ct)
        {
            await _mediator.Send(new SupplyCommand(supplyRequest), ct);
        }
    }
}
