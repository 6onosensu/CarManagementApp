using Car.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Car.Data
{
    public class CarDbContext: DbContext
    {
        public CarDbContext(DbContextOptions<CarDbContext> options) : base(options) { }
        public DbSet<CarEntity> Cars { get; set; }
        public DbSet<ServiceRecord> ServiceRecords { get; set; }
    }
}
