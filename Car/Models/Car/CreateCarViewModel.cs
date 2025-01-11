using System.ComponentModel.DataAnnotations;

namespace Car.Models.Car
{
    public class CreateCarViewModel
    {
        [Required]
        [StringLength(10, ErrorMessage = "Number plate length cannot exceed 10 characters.")]
        public string NumberPlate { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
    }
}
