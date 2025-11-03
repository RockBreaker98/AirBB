using Microsoft.AspNetCore.Mvc;
using AirBB.Models;
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

        public IActionResult Index()
        {
            var reservations = _context.Reservations
                .Include(r => r.Residence)
                .ThenInclude(r => r.Location)
                .ToList();

            return View(reservations);
        }

        [HttpPost]
        public IActionResult Reserve(int residenceId, DateTime startDate, DateTime endDate)
        {
            var residence = _context.Residences.Find(residenceId);
            if (residence == null)
                return NotFound();

            var reservation = new Reservation
            {
                ResidenceId = residenceId,
                ReservationStartDate = startDate,
                ReservationEndDate = endDate
            };

            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            TempData["Message"] = $"Reservation confirmed for {residence.Name}!";
            return RedirectToAction("Index", "Home");
        }

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
    }
}

