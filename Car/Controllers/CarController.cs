using Microsoft.AspNetCore.Mvc;
using Car.Models.Car;
using Car.Models;
using Car.Core.Domain;
using Car.Data;
using Microsoft.EntityFrameworkCore;
using Car.Models.ServiceRecord;
using Car.Core.ServiceInterface;

namespace Car.Controllers
{
    public class CarController : Controller
    {
        private readonly CarDbContext _context;
        private readonly ICarServices _services;
        public CarController(CarDbContext dbContext, ICarServices carServices)
        {
            _context = dbContext;
            _services = carServices;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _context.Cars
                .Select(car => new CarsIndexViewModel
                {
                    Id = car.Id,
                    NumberPlate = car.NumberPlate,
                    Make = car.Make,
                    Model = car.Model,
                    Year = car.Year,
                    Color = car.Color
                }).ToListAsync();

            return View(cars);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var car = await _services.Details(id);

            if (car == null)
            {
                return NotFound();
            }

            var model = new CarWithRecordsViewModel
            {
                Id = car.Id,
                NumberPlate = car.NumberPlate,
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                Color = car.Color,
                CreatedAt = car.CreatedAt,
                ModifiedAt = car.ModifiedAt,
                ServiceRecords = car.ServiceRecords
                    .Select(record => new RecordsViewModel
                {
                    Id = record.Id,
                    Title = record.Title,
                    Description = record.Description,
                    IsPass = record.IsPass,
                    CreatedAt = record.CreatedAt,
                    ModifiedAt = record.ModifiedAt
                }).ToList()
            };

            return View(model);
        }


    }
}
