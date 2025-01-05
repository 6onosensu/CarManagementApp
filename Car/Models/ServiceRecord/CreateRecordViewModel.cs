using System.ComponentModel.DataAnnotations;

namespace Car.Models.ServiceRecord
{
    public class CreateRecordViewModel
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPass { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Number plate length cannot exceed 10 characters.")]
        public string NumberPlate { get; set; }
    }
}
