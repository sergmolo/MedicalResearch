using MedicalResearch.Data.Enums;
using System;

namespace MedicalResearch.V1.Requests
{
    public class AddToStockRequest
    {
        public int MedicineId { get; set; }
        public MedicineState MedicineState { get; set; }
        public DateTime ExpireAt { get; set; }
        public int Amount { get; set; }
    }
}
