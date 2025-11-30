using System.ComponentModel.DataAnnotations.Schema;

namespace AirBB.Models.DomainModels
{
    public class Experience
    {
        public int ExperienceId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        
        public decimal DiscountPercent { get; set; } = 0m;

        [NotMapped]
        public decimal DiscountPrice => Math.Round(Price * (1 - (DiscountPercent / 100m)), 2);

        // FK
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
