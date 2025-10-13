namespace AirBB.Models
{
    public class Property
    {
        public int PropertyID { get; set; }   // Primary Key
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal PricePerNight { get; set; }

        // Foreign Key
        public int CategoryID { get; set; }
        public Category? Category { get; set; }
    }
}
