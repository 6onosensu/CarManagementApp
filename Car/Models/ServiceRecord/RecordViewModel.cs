using Car.Models.Car;
using System.ComponentModel.DataAnnotations;

namespace Car.Models.ServiceRecord
{
    public class RecordViewModel
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public CarViewModel CarVM { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        public string Description { get; set; } 
        public bool IsPass { get; set; } 
    }
}

