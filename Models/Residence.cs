using System.ComponentModel.DataAnnotations;

namespace AirBB.Models
{
    public class Residence
    {
        [Key]
        public int ResidenceId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string ResidencePicture { get; set; } = "placeholder.jpg";

        public int LocationId { get; set; }

        public Location? Location { get; set; }

        public int GuestNumber { get; set; }
        public int BedroomNumber { get; set; }
        public int BathroomNumber { get; set; }

        public decimal PricePerNight { get; set; }

        public List<Reservation>? Reservations { get; set; }
    }
}