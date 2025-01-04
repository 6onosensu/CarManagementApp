using Car.Core.Dto;
using Car.Core.Domain;

namespace Car.Core.ServiceInterface
{
    public interface ICarServices
    {
        Task<CarEntity> Details(Guid id);
    }
}
