// File: Models/Residence.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace AirBB.Models
{
    public class Residence
    {
        public int ResidenceId { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public int LocationId { get; set; }
        public Location? Location { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name must be less than 50 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Only letters and numbers allowed.")]
        public string Name { get; set; } = "";

        // Remote validation to ensure the Owner exists and is of type Owner
        [Required(ErrorMessage = "Owner ID is required.")]
        [Remote(action: "ValidateOwnerId", controller: "Residence",
            ErrorMessage = "Owner ID must exist and be of type 'Owner'.")]
        public int OwnerId { get; set; }
        public Client? Owner { get; set; }

        [Required(ErrorMessage = "Accommodation is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Accommodation must be an integer greater than 0.")]
        public int Accommodation { get; set; }

        [Required(ErrorMessage = "Bedrooms are required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Bedrooms must be a positive integer.")]
        public int Bedrooms { get; set; }

        // FIXED: Bathrooms must be integer only
        [Required(ErrorMessage = "Bathrooms are required.")]
        [Range(1, 50, ErrorMessage = "Bathrooms must be a whole number.")]
        public decimal Bathrooms { get; set; }

        // past year, not more than 150 years ago
        [Required(ErrorMessage = "Built year is required.")]
        [CustomValidation(typeof(Residence), nameof(ValidateBuiltYear))]
        public int BuiltYear { get; set; }

        public string? Image { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, 9999999, ErrorMessage = "Price must be numeric.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        // Legacy aliases (for older public pages)
        public string? ResidencePicture { get => Image; set => Image = value; }
        public int GuestNumber { get => Accommodation; set => Accommodation = value; }
        public int BedroomNumber { get => Bedrooms; set => Bedrooms = value; }

        // FIXED: BathroomNumber must map to Bathrooms
        public decimal BathroomNumber
        {
            get => Bathrooms;
            set => Bathrooms = value;
        }

        public decimal PricePerNight { get; set; }

        // Custom validator for BuiltYear
        public static ValidationResult? ValidateBuiltYear(int year, ValidationContext _)
        {
            int currentYear = DateTime.Now.Year;
            if (year >= currentYear)
                return new ValidationResult("Built year must be in the past.");
            if (currentYear - year > 150)
                return new ValidationResult("Built year cannot be more than 150 years old.");
            return ValidationResult.Success;
        }
    }
}
