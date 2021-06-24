using MedicalResearch.Data.Enums;
using System;

namespace MedicalResearch.Data.Entities
{
    public class Supply
    {
        public int Id { get; set; }
        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; } = default!;
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; } = default!;
        public MedicineState MedicineState { get; set; }
        public DateTime ExpireAt { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
