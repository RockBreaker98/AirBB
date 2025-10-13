namespace AirBB.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation
        public List<Experience>? Experiences { get; set; }
    }
}
