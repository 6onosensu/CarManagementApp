using Car.Core.Domain;

namespace Car.Models.ServiceRecord
{
    public class DeleteRecordViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPass { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public Guid CarId { get; set; }
    }
}
