using Car.Models.ServiceRecord;

namespace Car.Models.Car
{
    public class DeleteCarViewModel
    {
        public Guid Id { get; set; }
        public string NumberPlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public List<DeleteRecordViewModel> ServiceRecords { get; set; } = new List<DeleteRecordViewModel>();
    }
}
