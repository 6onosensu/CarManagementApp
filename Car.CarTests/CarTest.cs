using Car.Core.Dto;
using Car.Core.ServiceInterface;

namespace Car.CarTests
{
    public class CarTest : TestBase
    {
        [Fact]
        public async Task Should_AddCar_WhenDataProvided()
        {
            var car = MockCarData();

            var result = await Svc<ICarServices>().Create(car);

            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task Should_GetServiceRecordById_WhenIdIsValid()
        {
            var car = MockCarData();
            var createdCar = await Svc<ICarServices>().Create(car);

            var serviceRecord = new ServiceRecordDto
            {
                CarId = createdCar.Id,
                Title = "Annual Maintenance",
                Description = "Oil Change",
                IsPass = true,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            var createdServiceRecord = await Svc<IServiceRecords>().AddRecord(serviceRecord);

            var result = await Svc<IServiceRecords>().GetRecordById(createdServiceRecord.Id);

            Assert.NotNull(result);
            Assert.Equal(createdServiceRecord.Id, result.Id);
            Assert.Equal(createdCar.Id, result.CarId);
        }

        [Fact]
        public async Task Should_GetCar_ById_WhenCarExists()
        {
            var car = MockCarData();
            var createdCar = await Svc<ICarServices>().Create(car);

            var retrievedCar = await Svc<ICarServices>().Details(createdCar.Id);

            Assert.NotNull(retrievedCar);
            Assert.Equal(createdCar.Id, retrievedCar.Id);
            Assert.Equal("Ford", retrievedCar.Make);
            Assert.Equal("Focus", retrievedCar.Model);
            Assert.Equal(2015, retrievedCar.Year);
            Assert.Equal("White", retrievedCar.Color);
        }

        [Fact]
        public async Task Should_ReturnNull_WhenServiceRecordDoesNotExist()
        {
            Guid nonExistingId = Guid.NewGuid();

            var result = await Svc<IServiceRecords>().GetRecordById(nonExistingId);

            Assert.Null(result);
        }

        private CarDto MockCarData()
        {
            return new CarDto
            {
                NumberPlate = "456MJA",
                Make = "Ford",
                Model = "Focus",
                Year = 2015,
                Color = "White",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
        }

        private ServiceRecordDto MockServiceRecordData()
        {
            return new ServiceRecordDto
            {
                CarId = Guid.NewGuid(),
                Title = "Service Check",
                Description = "Tire Rotation",
                IsPass = true,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
        }
    }
}