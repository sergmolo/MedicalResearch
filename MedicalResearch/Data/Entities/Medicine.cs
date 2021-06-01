using System;

namespace MedicalResearch.Data.Entities
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int MedicineTypeId { get; set; }
        public MedicineType MedicineType { get; set; } = default!;
        public int DosageFormId { get; set; }
        public DosageForm DosageForm { get; set; } = default!;
        public int ContainerId { get; set; }
        public Container Container { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
