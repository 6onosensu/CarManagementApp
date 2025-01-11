using Car.Core.Domain;
using Car.Core.Dto;

namespace Car.Core.ServiceInterface
{
    public interface IServiceRecords
    {
        Task<ServiceRecord> AddRecord(ServiceRecordDto dto);
        Task<List<ServiceRecord>> GetRecordsByCarId(Guid carId);
        Task<ServiceRecord> GetRecordById(Guid id);
        Task<Guid> DeleteRecord(Guid id);
        Task<ServiceRecord> UpdateRecord(Guid recordId, ServiceRecordDto dto);
    }
}
