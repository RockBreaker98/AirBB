using System.ComponentModel.DataAnnotations;

namespace AirBB.Areas.Admin.Models
{
    public class AdminLocationViewModel
    {
        public int LocationId { get; set; }

        [Required, StringLength(50)]
        public string City { get; set; } = "";

        [StringLength(50)]
        public string? State { get; set; }

        [StringLength(50)]
        public string? Country { get; set; }
    }
}
