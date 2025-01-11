using Car.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Car.Data
{
    public class CarDbContext: DbContext
    {
        public CarDbContext(DbContextOptions<CarDbContext> options) : base(options) { }
        public DbSet<CarEntity> Cars { get; set; }
        public DbSet<ServiceRecord> ServiceRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceRecord>()
                .HasOne(sr => sr.CarEntity)
                .WithMany(c => c.ServiceRecords)
                .HasForeignKey(sr => sr.CarId)
                .OnDelete(DeleteBehavior.Cascade); 

            base.OnModelCreating(modelBuilder);
        }
    }
}
