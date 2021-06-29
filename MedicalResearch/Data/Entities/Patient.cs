using MedicalResearch.Data.Enums;
using System;
using System.Collections.Generic;

namespace MedicalResearch.Data.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public int PatientNumber { get; set; } = default!;
        public PatientSex Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IList<Visit> Visits { get; set; } = default!;
        public PatientStatus Status { get; set; }
        public int? MedicineTypeId { get; set; } = default!;
        public MedicineType? MedicineType { get; set; } = default!;
        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; } = default!;
        public DateTime ParticipationEndAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
