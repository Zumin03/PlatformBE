using InstrumentPlatform.Entities;
using Microsoft.EntityFrameworkCore;

namespace InstrumentPlatform.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<InstrumentEntity> Instruments { get; set; } = null!;
        public DbSet<MeasurementEntity> Measurements { get; set; } = null!;
        public DbSet<AuthorizedInstrumentEntity> Authorized { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MeasurementEntity>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<AuthorizedInstrumentEntity>()
                .HasKey(a => a.DeviceId);


            modelBuilder.Entity<InstrumentEntity>()
                .HasKey(i => i.DeviceId);

            modelBuilder.Entity<InstrumentEntity>()
                .HasOne<AuthorizedInstrumentEntity>()
                .WithMany()
                .HasForeignKey(i => i.DeviceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AuthorizedInstrumentEntity>().HasData(
                new AuthorizedInstrumentEntity { DeviceId = "TC-00000" },
                new AuthorizedInstrumentEntity { DeviceId = "H2-00000" }
            );
        }
    }
}