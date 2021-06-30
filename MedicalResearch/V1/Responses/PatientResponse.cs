using System;
using System.Collections.Generic;

namespace MedicalResearch.V1.Responses
{
    public class PatientResponse
    {
        public int ClinicId { get; set; }
        public int PatientNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime LastVisitDate { get; set; }
        public IEnumerable<PatientMedicineResponse> Medicines { get; set; } = default!;
    }
}
