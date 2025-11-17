using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AirBB.Models
{
    public class Client : IValidatableObject
    {
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(60)]
        public string Name { get; set; } = "";

            // Optional, but if provided must look like a phone
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [MinLength(10, ErrorMessage = "Phone number must be at least 10 digits.")]
        [MaxLength(10, ErrorMessage = "Phone number cannot exceed 10 digits.")]
        public string? PhoneNumber { get; set; }

        // Optional, but if provided must look like an email
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(100)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "SSN is required.")]
        [MinLength(9, ErrorMessage = "SSN must be at least 9 digits.")]
        [MaxLength(9, ErrorMessage = "SSN cannot exceed 9 digits.")]
        [Display(Name = "SSN")]
        [StringLength(9)]
        public string SSN { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "User Type is required.")]
        public UserType UserType { get; set; }

        // At least one of Phone or Email must be present
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber) && string.IsNullOrWhiteSpace(Email))
            {
                yield return new ValidationResult(
                    "Either a phone number or an email address must be provided.",
                    new[] { nameof(PhoneNumber), nameof(Email) }
                );
            }
        }
    }
}