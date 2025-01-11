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
        /*[HttpGet]
        public IActionResult AddRecordForm(Guid carId)
        {
            var model = new RecordViewModel { CarId = carId };
            return PartialView("~/Views/Record/_AddRecordForm", model);
        }*/

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

        [HttpGet]
        public async Task<IActionResult> UpdateRecordForm(Guid id)
        {
            var record = await _recordService.GetRecordById(id);
            if (record == null) return NotFound();

            var model = new RecordViewModel
            {
                Id = record.Id,
                CarId = record.CarId,
                Title = record.Title,
                Description = record.Description,
                IsPass = record.IsPass
            };
            return PartialView("~/Views/Record/_UpdateRecordForm", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRecord(RecordViewModel model)
        {
            var dto = new ServiceRecordDto
            {
                Title = model.Title,
                Description = model.Description,
                IsPass = model.IsPass
            };

            await _recordService.UpdateRecord(model.Id, dto);
            var records = _recordService.GetRecordsByCarId(model.CarId);
            return PartialView("~/Views/Record/_RecordsTable", records);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRecord(Guid id)
        {
            var carId = await _recordService.DeleteRecord(id);
            var records = await _recordService.GetRecordsByCarId(carId);

            return PartialView("~/Views/Record/_RecordsTable", records);
        }
    }

}
