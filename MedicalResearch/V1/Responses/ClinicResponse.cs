namespace MedicalResearch.V1.Responses
{
    public class ClinicResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Address2 { get; set; } = default!;
        public string Phone { get; set; } = default!;
    }
}
