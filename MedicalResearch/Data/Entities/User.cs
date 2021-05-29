using System;
using MedicalResearch.Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace MedicalResearch.Data.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Initials { get; set; } = default!;
        public bool IsRemoved { get; set; }
        public Role Role { get; set; }
        public int? ClinicId { get; set; }
        public Clinic? Clinic { get; set; }
        public DateTime PasswordCreatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
