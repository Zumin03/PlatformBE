using Microsoft.EntityFrameworkCore;
using PlatformTest.Entities;

namespace PlatformTest.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<InstrumentEntity>  instruments { get; set; } = null!;
        public DbSet<MeasurementEntity> measurements { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MeasurementEntity>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();
        }

    }
}