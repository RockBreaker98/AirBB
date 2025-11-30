using Microsoft.AspNetCore.Mvc;
using AirBB.Models;
using AirBB.Models.DataLayer;
using AirBB.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Controllers
{
    public class ReservationController : Controller
    {
        private readonly AirBBContext _context;
        
        public ReservationController(AirBBContext context)
        {
            _context = context;
        }

        // ============================
        // LIST ALL RESERVATIONS
        // ============================
        public IActionResult Index()
        {
            var reservations = _context.Reservations
                .Include(r => r.Residence)
                .ThenInclude(r => r.Location)
                .ToList();

            return View(reservations);
        }


        // ============================
        // RESERVE POST
        // ============================
        [HttpPost]
        public IActionResult Reserve(int residenceId, DateTime startDate, DateTime endDate)
        {
            var residence = _context.Residences.Find(residenceId);
            if (residence == null)
                return NotFound();

            // ---- VALIDATIONS ----
            string? error = ValidateReservation(residenceId, startDate, endDate);
            if (error != null)
            {
                TempData["ErrorMessage"] = error;
                return RedirectToAction("Details", "Home", new { id = residenceId });
            }

            // ---- SAVE RESERVATION ----
            var reservation = new Reservation
            {
                ResidenceId = residenceId,
                ReservationStartDate = startDate,
                ReservationEndDate = endDate
            };

            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            TempData["Message"] = $"Reservation confirmed for {residence.Name} from {startDate:d} to {endDate:d}.";
            return RedirectToAction("Index", "Home");
        }


        // ============================
        // CANCEL RESERVATION
        // ============================
        [HttpPost]
        public IActionResult Cancel(int id)
        {
            var reservation = _context.Reservations.Find(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        // ======================================================
        // PRIVATE VALIDATION METHOD (per professor: refactoring)
        // ======================================================
        private string? ValidateReservation(int residenceId, DateTime start, DateTime end)
        {
            // Same day NOT allowed
            if (start.Date == end.Date)
                return "Start and end dates cannot be the same.";

            // Must be a minimum 24 hours
            if ((end - start).TotalHours < 24)
                return "Reservation must be at least 24 hours.";

            // Overlap check
            bool overlaps = _context.Reservations.Any(r =>
                r.ResidenceId == residenceId &&
                start < r.ReservationEndDate &&
                end > r.ReservationStartDate);

            if (overlaps)
                return "This residence is already booked for the selected dates.";

            return null;
        }
    }
}
