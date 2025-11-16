using System.ComponentModel.DataAnnotations;

namespace AirBB.Areas.Admin.Models
{
    public class AdminUserViewModel
    {
        public int AppUserId { get; set; }

        [Required, StringLength(60)]
        public string Name { get; set; } = "";

        [Phone(ErrorMessage = "Invalid phone number format.")]
        [MinLength(10, ErrorMessage = "Phone number must be at least 10 digits.")]
        [MaxLength(10, ErrorMessage = "Phone number cannot exceed 10 digits.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "SSN is required.")]
        [MinLength(10, ErrorMessage = "SSN must be at least 9 digits.")]
        [MaxLength(10, ErrorMessage = "SSN cannot exceed 9 digits.")]
        [Display(Name = "SSN")]
        [StringLength(9)]
        public string? SSN { get; set; }

        [Required]
        public string UserType { get; set; } = string.Empty;
    }
}