using MedicalResearch.Data.Enums;
using System;

namespace MedicalResearch.Data.Entities
{
    public class Stock
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; } = default!;
        public MedicineState MedicineState { get; set; }
        public DateTime ExpireAt { get; set; }
        public int Amount { get; set; }
    }
}
