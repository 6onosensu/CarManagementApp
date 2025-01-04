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

        public async Task<ServiceRecord> AddRecord(ServiceRecordDto dto, Guid carId)
        {
            var serviceRecord = new ServiceRecord
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                IsPass = dto.IsPass,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                CarId = carId
            };

            await _context.ServiceRecords.AddAsync(serviceRecord);
            await _context.SaveChangesAsync();

            return serviceRecord;
        }

        public async Task<List<ServiceRecord>> GetRecordsByCarId(Guid carId)
        {
            var records = await _context.ServiceRecords
                .Where(record => record.CarId == carId)
                .ToListAsync();
            return records;
        }

        public async Task<ServiceRecord> GetRecordById(Guid id)
        {
            var record = await _context.ServiceRecords
                .FirstOrDefaultAsync(x => x.Id == id);

            return record;
        }

        public async Task DeleteRecord(Guid recordId)
        {
            var record = await _context.ServiceRecords.FindAsync(recordId);
            if (record != null)
            {
                _context.ServiceRecords.Remove(record);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ServiceRecord> UpdateRecord(Guid recordId, ServiceRecordDto dto)
        {
            var record = await _context.ServiceRecords.FindAsync(recordId);

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