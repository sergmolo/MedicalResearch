namespace MedicalResearch.V1.Requests
{
    public class AddMedicineRequest
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int MedicineTypeId { get; set; } = default!;
        public int DosageFormId { get; set; } = default!;
        public int ContainerId { get; set; } = default!;
    }
}
