using Car.Core.Domain;

namespace Car.Models.ServiceRecord
{
    public class DeleteRecordViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPass { get; set; }
        public Guid CarId { get; set; }
        public CarEntity CarEntity { get; set; }
    }
}
