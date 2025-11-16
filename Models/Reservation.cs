using System.ComponentModel.DataAnnotations;

namespace AirBB.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        [CustomValidation(typeof(Reservation), nameof(ValidateDates))]
        public DateTime ReservationStartDate { get; set; }

        public DateTime ReservationEndDate { get; set; }

        public int ResidenceId { get; set; }
        public Residence Residence { get; set; } = default!;

        // ---- DATE VALIDATION ----
        public static ValidationResult? ValidateDates(Reservation model, ValidationContext context)
        {
            // Reject same date
            if (model.ReservationStartDate.Date == model.ReservationEndDate.Date)
            {
                return new ValidationResult(
                    "Start and end dates cannot be the same. Minimum stay is 1 night."
                );
            }

            // Must be at least 1 full day apart (24 hours)
            if ((model.ReservationEndDate - model.ReservationStartDate).TotalHours < 24)
            {
                return new ValidationResult(
                    "Reservation must be at least 1 full day (24 hours)."
                );
            }

            return ValidationResult.Success;
        }
    }
}


