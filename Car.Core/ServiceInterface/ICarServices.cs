using Car.Core.Dto;
using Car.Core.Domain;

namespace Car.Core.ServiceInterface
{
    public interface ICarServices
    {
        Task<CarEntity> Details(Guid id);
        Task<CarEntity> Create(CarDto dto);
        Task AddServiceRecordToCar(Guid carId, ServiceRecordDto recordDto);
    }
}
