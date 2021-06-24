using MedicalResearch.Data.Enums;
using System;

namespace MedicalResearch.V1.Responses
{
    public class StockResponse
    {
        public int Id { get; set; }
        public MedicineResponse Medicine { get; set; } = default!;
        public MedicineState MedicineState { get; set; }
        public DateTime ExpireAt { get; set; }
        public int Amount { get; set; }
    }
}
