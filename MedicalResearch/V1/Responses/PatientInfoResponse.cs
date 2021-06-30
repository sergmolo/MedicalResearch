using MedicalResearch.Data.Enums;
using System;

namespace MedicalResearch.V1.Responses
{
    public class PatientInfoResponse
    {
        public int ClinicId { get; set; }
        public int PatientNumber { get; set; }
        public PatientStatus Status { get; set; }
        public PatientSex Sex { get; set; }
        public string MedicineType { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public DateTime LastVisitDate { get; set; }
    }
}
