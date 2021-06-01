using MediatR;
using MedicalResearch.V1.Requests;

namespace MedicalResearch.Business.Commands.Medicines
{
    public class AddMedicineCommand : IRequest<Unit>
    {
        public AddMedicineRequest Model { get; }

        public AddMedicineCommand(AddMedicineRequest model)
        {
            Model = model;
        }
    }
}
