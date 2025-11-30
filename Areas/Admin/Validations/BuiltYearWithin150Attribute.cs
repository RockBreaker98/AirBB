using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AirBB.Areas.Admin.Validation
{
    // Ensures BuiltYear is not in the future and not older than 150 years
    public class BuiltYearWithin150Attribute : ValidationAttribute, IClientModelValidator
    {
        private const int MaxAgeYears = 150;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(ErrorMessage ?? "Built year is required.");
            }

            if (!int.TryParse(value.ToString(), out int builtYear))
            {
                return new ValidationResult("Invalid built year format.");
            }

            int currentYear = DateTime.Now.Year;
            int oldestAllowed = currentYear - MaxAgeYears;

            if (builtYear < oldestAllowed || builtYear > currentYear)
            {
                return new ValidationResult(
                    ErrorMessage ?? $"Built year must be between {oldestAllowed} and {currentYear}."
                );
            }

            return ValidationResult.Success;
        }

        // CLIENT-SIDE VALIDATION: Adds data-val-* attributes
        public void AddValidation(ClientModelValidationContext context)
        {
            int currentYear = DateTime.Now.Year;
            int oldestAllowed = currentYear - MaxAgeYears;

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes,
                "data-val-builtyearwithin150",
                ErrorMessage ?? $"Built year must be between {oldestAllowed} and {currentYear}.");

            MergeAttribute(context.Attributes, "data-val-builtyearwithin150-minyear", oldestAllowed.ToString());
            MergeAttribute(context.Attributes, "data-val-builtyearwithin150-maxyear", currentYear.ToString());
        }

        private static bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
                return false;

            attributes.Add(key, value);
            return true;
        }
    }
}
