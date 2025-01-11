using Car.Core.Domain;
using Car.Core.Dto;
using Car.Core.ServiceInterface;
using Car.Data;
using Microsoft.EntityFrameworkCore;

namespace Car.ApplicationServices.Services
{
    public class ServiceRecordsServices: IServiceRecords
    {
        private readonly CarDbContext _context;
        public ServiceRecordsServices (CarDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceRecord> AddRecord(ServiceRecordDto dto)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == dto.CarId);
            if (car == null)
            {
                throw new Exception("The car with the provided ID does not exist.");
            }

            var serviceRecord = new ServiceRecord
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                IsPass = dto.IsPass,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                CarId = dto.CarId,
                CarEntity = car
            };

            await _context.ServiceRecords.AddAsync(serviceRecord);
            await _context.SaveChangesAsync();

            return serviceRecord;
        }

        public async Task<List<ServiceRecord>> GetRecordsByCarId(Guid carId)
        {
            return await _context.ServiceRecords
                .Where(r => r.CarId == carId)
                .ToListAsync();
        }

        public async Task<ServiceRecord> GetRecordById(Guid id)
        {
            return await _context.ServiceRecords.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Guid> DeleteRecord(Guid id)
        {
            var record = await _context.ServiceRecords.FindAsync(id);
            if (record != null)
            {
                var carId = record.CarId;

                _context.ServiceRecords.Remove(record);
                await _context.SaveChangesAsync();

                return carId;
            }
            throw new KeyNotFoundException($"Record with ID {id} not found.");
        }


        public async Task<ServiceRecord> UpdateRecord(Guid recordId, ServiceRecordDto dto)
        {
            var record = await _context.ServiceRecords.FindAsync(recordId);

            if (record == null)
            {
                throw new Exception("Record not found");
            }

            record.Title = dto.Title;
            record.Description = dto.Description;
            record.IsPass = dto.IsPass;
            record.ModifiedAt = DateTime.Now;

            _context.ServiceRecords.Update(record);
            await _context.SaveChangesAsync();

            return record;
        }
    }
}