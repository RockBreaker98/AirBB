using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirBB.Models;
using AirBB.Models.DataLayer;
using AirBB.Models.DomainModels;

namespace AirBB.Controllers
{
    public class ResidenceController : Controller
    {
        private readonly AirBBContext _context;

        public ResidenceController(AirBBContext context)
        {
            _context = context;
        }

        // -------------------------------------------------------
        // PUBLIC: List of residences (READ-ONLY)
        // -------------------------------------------------------
        public IActionResult Index()
        {
            var residences = _context.Residences
                .Include(r => r.Location)
                .Include(r => r.Owner)
                .OrderBy(r => r.Name)
                .ToList();

            return View(residences);
        }

        // -------------------------------------------------------
        // PUBLIC: Show details for one residence (optional)
        // -------------------------------------------------------
        public IActionResult Details(int id)
        {
            var residence = _context.Residences
                .Include(r => r.Location)
                .Include(r => r.Owner)
                .FirstOrDefault(r => r.ResidenceId == id);

            if (residence == null)
                return NotFound();

            return View(residence);
        }

    
    }
}
