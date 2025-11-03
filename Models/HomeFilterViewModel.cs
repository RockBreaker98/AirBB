using System;
using System.Collections.Generic;

namespace AirBB.Models
{
    public class HomeFilterViewModel
    {
        public int? ActiveLocationId { get; set; }
        public int Guests { get; set; } = 1;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // init to empty so Razor never sees null
        public List<Location> Locations { get; set; } = new();
        public List<Residence> Residences { get; set; } = new();
    }
}

