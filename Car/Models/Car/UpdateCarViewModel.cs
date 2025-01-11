using Car.Models.ServiceRecord;
using System.ComponentModel.DataAnnotations;

namespace Car.Models.Car
{
    public class UpdateCarViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Number plate length cannot exceed 10 characters.")]
        public string NumberPlate { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [Range(1880, 2100)]
        public int Year { get; set; }

        [Required]
        public string Color { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public List<RecordsViewModel> ServiceRecords { get; set; }
    }
}
