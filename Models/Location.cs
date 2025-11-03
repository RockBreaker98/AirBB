using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AirBB.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }   // Primary Key

        [Required]
        public string Name { get; set; } = string.Empty;

        // Navigation property: a location can have many residences
        public List<Residence>? Residences { get; set; }
    }
}
