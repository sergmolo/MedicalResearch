using MedicalResearch.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MedicalResearch.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Clinic> Clinics { get; set; } = default!;
        public DbSet<DosageForm> DosageForms { get; set; } = default!;
        public DbSet<Container> Containers { get; set; } = default!;
        public DbSet<MedicineType> MedicineTypes { get; set; } = default!;
        public DbSet<Medicine> Medicines { get; set; } = default!;

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasPostgresExtension("citext");
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            builder.Ignore<IdentityRole<int>>();
            builder.Ignore<IdentityUserLogin<int>>();
            builder.Ignore<IdentityUserToken<int>>();
            builder.Ignore<IdentityUserRole<int>>();
            builder.Ignore<IdentityRoleClaim<int>>();
            builder.Ignore<IdentityUserClaim<int>>();
        }
    }
}
