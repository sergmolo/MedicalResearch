using MediatR;
using MedicalResearch.V1.Requests;

namespace MedicalResearch.Business.Commands.Stock
{
    public class AddToStockCommand : IRequest<Unit>
    {
        public AddToStockRequest Model { get; }

        public AddToStockCommand(AddToStockRequest model)
        {
            Model = model;
        }
    }
}
