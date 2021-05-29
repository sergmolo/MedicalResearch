using System;

namespace MedicalResearch.Data.Entities
{
    public class Clinic
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Address2 { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
