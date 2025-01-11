using Microsoft.AspNetCore.Mvc;
using Car.Models.Car;
using Car.Models;
using Car.Core.Domain;
using Car.Data;
using Microsoft.EntityFrameworkCore;
using Car.Models.ServiceRecord;
using Car.Core.ServiceInterface;
using Car.Core.Dto;

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
        public IActionResult Create()
        {
            CreateCarViewModel car = new();
            return View("Create", car);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCarViewModel vm)
        {
            var dto = new CarDto()
            {
                Id = new Guid(),
                NumberPlate = vm.NumberPlate,
                Make = vm.Make,
                Model = vm.Model,
                Year = vm.Year,
                Color = vm.Color,
            };

            var result = await _services.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);
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
                ServiceRecords = (car.ServiceRecords ?? new List<ServiceRecord>())
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

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var car = await _services.Details(id);
            if (car == null)
            {
                return NotFound();
            }

            var vm = new UpdateCarViewModel()
            {
                Id = car.Id,
                NumberPlate = car.NumberPlate,
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                Color = car.Color,
                CreatedAt = car.CreatedAt,
                ModifiedAt = car.ModifiedAt,
            };

            return View("_UpdateCarForm", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCarViewModel vm)
        {
            var carDto = new CarDto()
            {
                Id = vm.Id,
                NumberPlate = vm.NumberPlate,
                Make = vm.Make,
                Model = vm.Model,
                Year = vm.Year,
                Color = vm.Color,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = DateTime.Now
            };

            var car = await _services.Update(carDto);

            if (car == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
