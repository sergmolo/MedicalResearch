using MedicalResearch.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalResearch.V1.Requests
{
    public class AddPatientRequest
    {
        public int ClinicId { get; set; }
        public int PatientNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public PatientSex Sex { get; set; }

        [DataType(DataType.Date)]
        public DateTime ParticipationEndAt { get; set; }
    }
}
