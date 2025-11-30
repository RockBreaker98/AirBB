using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AirBB.Models.DomainModels;

namespace AirBB.Models.ViewModels
{
    public class HomeFilterViewModel
    {
        // incoming filters (via query/form)
        public int? ActiveLocationId { get; set; }
        public int Guests { get; set; } = 1;

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        // used by the "Clear" button
        public bool Clear { get; set; }

        // data for the page
        public List<Location> Locations { get; set; } = new();
        public List<Residence> Residences { get; set; } = new();
    }
}