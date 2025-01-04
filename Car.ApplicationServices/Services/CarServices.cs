using Car.Core.Dto;
using Car.Core.ServiceInterface;
using Car.Data;
using Microsoft.EntityFrameworkCore;
using Car.Core.Domain;

namespace Car.ApplicationServices.Services
{
    public class CarServices: ICarServices
    {
        private readonly CarDbContext _context;
        private readonly IServiceRecords _records;
        public CarServices(CarDbContext context, IServiceRecords records) 
        {
            _context = context;
            _records = records;
        }

        public async Task<CarEntity> Details(Guid id)
        {
            var result = await _context.Cars
                .Include(x => x.ServiceRecords)
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<CarEntity> Create(CarDto dto)
        {
            CarEntity car = new CarEntity()
            {
                Id = Guid.NewGuid(),
                NumberPlate = dto.NumberPlate,
                Make = dto.Make,
                Model = dto.Model,
                Year = dto.Year,
                Color = dto.Color,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            return car;
        }

        public async Task AddServiceRecordToCar(Guid carId, ServiceRecordDto recordDto)
        {
            await _records.AddRecord(recordDto, carId);
        }
    }
}
