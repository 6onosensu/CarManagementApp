using Car.Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace Car.Models.Car
{
    public class CreateCarViewModel
    {
        [Required]
        public string NumperPlate { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
    }
}
