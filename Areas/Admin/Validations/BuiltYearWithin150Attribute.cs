using System;
using System.ComponentModel.DataAnnotations;

namespace AirBB.Areas.Admin.Validation
{
    // Custom validator to ensure BuiltYear is not older than 150 years and not in the future
    public class BuiltYearWithin150Attribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Built year is required.");

            if (int.TryParse(value.ToString(), out int builtYear))
            {
                int currentYear = DateTime.Now.Year;
                int oldestAllowed = currentYear - 150;

                if (builtYear < oldestAllowed || builtYear > currentYear)
                {
                    return new ValidationResult(
                        $"Built year must be between {oldestAllowed} and {currentYear}."
                    );
                }

                return ValidationResult.Success!;
            }

            return new ValidationResult("Invalid built year format.");
        }
    }
}