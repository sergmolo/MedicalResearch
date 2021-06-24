namespace MedicalResearch.V1.Requests
{
    public class SupplyRequest
    {
        public int ClinicId { get; set; }
        public int MedicineId { get; set; }
        public int Amount { get; set; }
    }
}
