// File: Controllers/ResidenceController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirBB.Models;
using System.Linq;

namespace AirBB.Controllers
{
    public class ResidenceController : Controller
    {
        private readonly AirBBContext _context;

        public ResidenceController(AirBBContext context)
        {
            _context = context;
        }

        // Remote validation: OwnerId must exist & be of type Owner
        [AcceptVerbs("GET", "POST")]
        public IActionResult ValidateOwnerId(int OwnerId)
        {
            var user = _context.Clients.FirstOrDefault(c => c.ClientId == OwnerId);

            if (user == null || user.UserType != UserType.Owner)
            {
                return Json($"User ID {OwnerId} does not exist or is not an Owner.");
            }

            return Json(true);
        }

        // List
        public IActionResult Index()
        {
            var residences = _context.Residences
                .Include(r => r.Location)
                .Include(r => r.Owner)
                .ToList();

            return View(residences);
        }

        // GET: Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create → Goes to Confirmation Page
        [HttpPost]
        public IActionResult Create(Residence residence)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please fix the validation errors before saving.";
                return View(residence);
            }

            // Server-side owner safety check (if JS disabled)
            var owner = _context.Clients
                .FirstOrDefault(c => c.ClientId == residence.OwnerId && c.UserType == UserType.Owner);

            if (owner == null)
            {
                ModelState.AddModelError("OwnerId", "The specified OwnerId does not exist or is not an Owner.");
                TempData["ErrorMessage"] = "Owner validation failed.";
                return View(residence);
            }

            // NEW: Move to confirmation page instead of saving immediately
            return View("ConfirmCreate", residence);
        }

        // POST: Confirmation page → Save to DB
        [HttpPost]
        public IActionResult ConfirmCreate(Residence residence)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please fix the validation errors before saving.";
                return View("Create", residence);
            }

            var owner = _context.Clients
                .FirstOrDefault(c => c.ClientId == residence.OwnerId && c.UserType == UserType.Owner);

            if (owner == null)
            {
                ModelState.AddModelError("OwnerId", "The specified OwnerId does not exist or is not an Owner.");
                TempData["ErrorMessage"] = "Owner validation failed.";
                return View("Create", residence);
            }

            _context.Residences.Add(residence);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Residence created successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
