using MediatR;
using MedicalResearch.V1.Requests;
using System.Collections.Generic;

namespace MedicalResearch.Business.Commands.Stock
{
    public class SupplyCommand : IRequest<Unit>
    {
        public IEnumerable<SupplyRequest> Models { get; }

        public SupplyCommand(IEnumerable<SupplyRequest> models)
        {
            Models = models;
        }
    }
}
