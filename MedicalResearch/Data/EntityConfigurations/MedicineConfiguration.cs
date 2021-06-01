using MedicalResearch.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalResearch.Data.EntityConfigurations
{
    public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> builder)
        {
            builder.Property(p => p.Name).HasColumnType("citext").IsRequired();
            
            builder.HasIndex(m => new { m.Name, m.MedicineTypeId, m.ContainerId, m.DosageFormId }).IsUnique();

            builder.HasOne(e => e.MedicineType).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Container).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.DosageForm).WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
