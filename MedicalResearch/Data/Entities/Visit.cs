using System;

namespace MedicalResearch.Data.Entities
{
    public class Visit
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = default!;
        public int? MedicineId { get; set; }
        public Medicine? Medicine { get; set; } = default!;
        public DateTime Date { get; set; }
    }
}
