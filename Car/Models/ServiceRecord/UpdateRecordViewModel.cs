namespace Car.Models.ServiceRecord
{
    public class UpdateRecordViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPass { get; set; }
        public string NumberPlate { get; set; }
    }
}
