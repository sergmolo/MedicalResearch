using MedicalResearch.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalResearch.Data.EntityConfigurations
{
    public class DosageFormConfiguration : IEntityTypeConfiguration<DosageForm>
    {
        public void Configure(EntityTypeBuilder<DosageForm> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnType("citext")
                .IsRequired();

            builder.HasIndex(p => p.Name)
                .IsUnique();
        }
    }
}
