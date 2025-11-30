using System.ComponentModel.DataAnnotations;

namespace AirBB.Models.DomainModels
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        [Required, StringLength(50)]
        public string City { get; set; } = "";

        [StringLength(50)]
        public string? State { get; set; }

        [StringLength(50)]
        public string? Country { get; set; }

        // Optional helper property for dropdown display
        public string DisplayName => $"{City}, {State}";

        // Legacy alias for old code references (do not remove old public pages)
        public string? Name
        {
            get => City;
            set => City = value ?? string.Empty;
        }

         public ICollection<Residence>? Residences { get; set; } = new List<Residence>();
    }
}

