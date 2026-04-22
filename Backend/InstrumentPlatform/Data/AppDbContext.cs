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
        public DbSet<AuthorizedInstrument> Authorized { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MeasurementEntity>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<AuthorizedInstrument>()
                .HasKey(a => a.InstrumentId);


            modelBuilder.Entity<InstrumentEntity>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<InstrumentEntity>()
                .HasOne<AuthorizedInstrument>()
                .WithMany()
                .HasForeignKey(i => i.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AuthorizedInstrument>().HasData(
                new AuthorizedInstrument { InstrumentId = "TC-00000" },
                new AuthorizedInstrument { InstrumentId = "H2-00000" }
            );
        }
    }
}