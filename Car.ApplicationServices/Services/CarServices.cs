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

        public async Task<CarDto> Details(Guid id)
        {
            var car = await _context.Cars
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
            if (car == null) return null;

            return new CarDto
            {
                Id = car.Id,
                NumberPlate = car.NumberPlate,
                Model = car.Model,
                Make = car.Make,
                Year = car.Year,
                Color = car.Color,
                CreatedAt = car.CreatedAt,
                ModifiedAt = car.ModifiedAt
            };
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
                ModifiedAt = DateTime.Now,
                ServiceRecords = new List<ServiceRecord>(),
            };

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            return car;
        }

        public async Task AddServiceRecordToCar(Guid carId, ServiceRecordDto recordDto)
        {
            await _records.AddRecord(recordDto, carId);
        }

        public async Task<bool> Delete(Guid id)
        {
            var car = await _context.Cars
                .Include(c => c.ServiceRecords)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                return false;
            }

            _context.ServiceRecords.RemoveRange(car.ServiceRecords);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<CarEntity> Update(CarDto dto)
        {
            var car = new CarEntity()
            {
                Id = dto.Id,
                NumberPlate = dto.NumberPlate,
                Make = dto.Make,
                Model = dto.Model,
                Year = dto.Year,
                Color = dto.Color,
                ModifiedAt = DateTime.Now,
            };

            _context.Cars.Update(car);
            await _context.SaveChangesAsync();

            return car;
        }
    }
}
