using System.ComponentModel.DataAnnotations;

namespace Car.Models.Car
{
    public class UpdateCarViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(10)]
        public string NumberPlate { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
    }
}
