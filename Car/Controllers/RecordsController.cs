using Car.Core.Domain;
using Car.Core.Dto;
using Car.Core.ServiceInterface;
using Car.Models;
using Car.Models.ServiceRecord;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Car.Controllers
{
    public class RecordsController : Controller
    {
        private IServiceRecords _recordService;
        private ICarServices _services;
        public RecordsController (IServiceRecords serviceRecords, ICarServices services)
        {
            _recordService = serviceRecords;
            _services = services;
        }

        [HttpPost]
        public async Task<IActionResult> AddRecordToCar(RecordViewModel model)
        {
            var dto = new ServiceRecordDto
            {
                Title = model.Title,
                Description = model.Description,
                IsPass = model.IsPass,
                CarId = model.CarId,
            };

            await _recordService.AddRecord(dto);
            var records = await _recordService.GetRecordsByCarId(model.CarId);

            var vmRecords = records.Select(record => new RecordsViewModel
            {
                Id = record.Id,
                Title = record.Title,
                Description = record.Description,
                IsPass = record.IsPass,
                CreatedAt = record.CreatedAt,
                ModifiedAt = record.ModifiedAt,
                CarId = record.CarId,
            }).ToList();

            var car = await _services.Details(dto.CarId);
            return RedirectToAction("Details", "Car", new { id = car.Id });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRecord(RecordViewModel model)
        {
            var dto = new ServiceRecordDto
            {
                Title = model.Title,
                Description = model.Description,
                IsPass = model.IsPass,
                CarId = model.CarId,
            };

            await _recordService.UpdateRecord(model.Id, dto);
            return RedirectToAction("Details", "Car", new { id = model.CarId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRecord(Guid id)
        {
            var carId = await _recordService.DeleteRecord(id);

            return RedirectToAction("Details", "Car", new { id = carId });
        }
    }

}
