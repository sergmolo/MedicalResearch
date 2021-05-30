using MediatR;
using MedicalResearch.V1.Requests;

namespace MedicalResearch.Business.Commands.Medicines
{
    public class EditMedicineCommand : IRequest<Unit>
    {
        public int Id { get; }
        public EditMedicineRequest Model { get; }

        public EditMedicineCommand(int id, EditMedicineRequest model)
        {
            Id = id;
            Model = model;
        }
    }
}
