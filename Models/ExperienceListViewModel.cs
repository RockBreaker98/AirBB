using System.Collections.Generic;

namespace AirBB.Models
{
    public class ExperienceListViewModel
    {
        public List<Category> Categories { get; set; } = new();
        public List<Experience> Experiences { get; set; } = new();
        public string CurrentCategory { get; set; } = "All";
    }
}
