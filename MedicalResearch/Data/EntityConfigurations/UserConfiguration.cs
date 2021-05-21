using MedicalResearch.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalResearch.Data.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.UserName)
                .IsRequired();

            builder.Property(p => p.Email)
                .IsRequired();

            builder.ToTable("Users");
        }
    }
}
