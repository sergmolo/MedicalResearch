namespace MedicalResearch.V1.Requests
{
    public class AddMedicineRequest
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int MedicineTypeId { get; set; }
        public int DosageFormId { get; set; }
        public int ContainerId { get; set; }
    }
}
