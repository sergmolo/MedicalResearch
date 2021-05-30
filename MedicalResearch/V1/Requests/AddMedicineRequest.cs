namespace MedicalResearch.V1.Requests
{
    public class AddMedicineRequest
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string MedicineType { get; set; } = default!;
        public string DosageForm { get; set; } = default!;
        public string Container { get; set; } = default!;
    }
}
