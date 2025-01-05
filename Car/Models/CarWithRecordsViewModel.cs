using Car.Models.Car;
using Car.Models.ServiceRecord;

namespace Car.Models
{
    public class CarWithRecordsViewModel
    {
        public Guid Id { get; set; }
        public string NumberPlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public List<RecordsViewModel> ServiceRecords { get; set; }
    }
}
