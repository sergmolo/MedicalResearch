using MediatR;

namespace MedicalResearch.Business.Commands.Medicines
{
    public class DeleteMedicineCommand : IRequest<Unit>
    {
        public int Id { get; }

        public DeleteMedicineCommand(int id)
        {
            Id = id;
        }
    }
}
