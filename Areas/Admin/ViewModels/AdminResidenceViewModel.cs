using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using AirBB.Areas.Admin.Validation;

namespace AirBB.Areas.Admin.Models
{
    public class AdminResidenceViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ResidenceId { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [Display(Name = "Location")]
        public int LocationId { get; set; }   // non-nullable => no blank option in Edit

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name must be less than 50 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-]+$", ErrorMessage = "Name must be alphanumeric only.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "OwnerId is required.")]
        [Display(Name = "Owner Id")]
        [Remote(action: "ValidateOwnerId", controller: "Users", areaName: "Admin",
                ErrorMessage = "OwnerId must exist and be an Owner.")]
        public int OwnerId { get; set; }

        [Required(ErrorMessage = "Accommodation is required.")]
        [Range(1, 50, ErrorMessage = "Accommodation must be between 1 and 50.")]
        [Display(Name = "Guests")]
        public int Accommodation { get; set; }

        [Required(ErrorMessage = "Bedrooms are required.")]
        [Range(0, 30, ErrorMessage = "Bedrooms must be between 0 and 30.")]
        public int Bedrooms { get; set; }

        [Required(ErrorMessage = "Bathrooms are required.")]
        [Range(0, 30, ErrorMessage = "Bathrooms must be between 0 and 30.")]
        [RegularExpression(@"^\d+(\.5)?$", ErrorMessage = "Bathrooms must be integer or end with .5.")]
        public decimal Bathrooms { get; set; }

        [Required(ErrorMessage = "Built year is required.")]
        [Display(Name = "Built Year")]
        [BuiltYearWithin150]
        public int BuiltYear { get; set; }

        public string? Image { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, 1000000, ErrorMessage = "Price must be between 0 and 1,000,000.")]
        [Display(Name = "Price / Night")]
        public decimal Price { get; set; }
    }
}