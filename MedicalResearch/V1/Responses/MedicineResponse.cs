namespace MedicalResearch.V1.Responses
{
    public class MedicineResponse
    {
        public int Id { get; set; }
        public string MedicineType { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string DosageForm { get; set; } = default!;
        public string Container { get; set; } = default!;
    }
}
