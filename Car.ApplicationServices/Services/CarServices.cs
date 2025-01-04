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
        public CarServices(CarDbContext context) 
        {
            _context = context;
        }

        public async Task<CarEntity> Details(Guid id)
        {
            var result = await _context.Cars.FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<CarEntity> Create(CarDto dto)
        {
            CarEntity carDto = new CarEntity()
            {
                Id = Guid.NewGuid(),
                NumberPlate = dto.NumberPlate,
                Make = dto.Make,
                Model = dto.Model,
                Year = dto.Year,
                Color = dto.Color,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };

            if (dto.ServiceRecords != null)
            {

            }


        }
    }
}
