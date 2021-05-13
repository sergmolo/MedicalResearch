using MedicalResearch.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MedicalResearch
{
    public class ApplicationDbContext : IdentityDbContext<User, ApplicationRole, int>
    //public class ApplicationDbContext
    //    : IdentityDbContext<User, ApplicationRole, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
    //IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DbSet<Medicine> Medicines { get; set; } = default!;
        public DbSet<DosageForm> DosageForms { get; set; } = default!;
        public DbSet<Container> Containers { get; set; } = default!;
        public DbSet<MedicineType> MedicineTypes { get; set; } = default!;
        public DbSet<Clinic> Clinics { get; set; } = default!;
        public DbSet<Patient> Patients { get; set; } = default!;
        public DbSet<Visit> Visits { get; set; } = default!;
        public DbSet<Supply> Supply { get; set; } = default!;
        public DbSet<Stock> Stock { get; set; } = default!;

        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("ApplicationContext"));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

			builder.Entity<ApplicationRole>()
				.HasMany(p => p.Users)
				.WithOne()
				.HasForeignKey(p => p.RoleId);
		}
    }
}
