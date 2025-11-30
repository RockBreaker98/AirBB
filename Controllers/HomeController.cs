using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;         // for Session GetInt32/SetInt32
using AirBB.Models;
using AirBB.Models.DataLayer;
using AirBB.Models.ViewModels;
using AirBB.Models.Utilities;

namespace AirBB.Controllers
{
    public class HomeController : Controller
    {
        private readonly AirBBContext _context;

        // session keys – store ONLY primitive values here
        private const string SessLocation = "airbb-location";
        private const string SessGuests   = "airbb-guests";
        private const string SessStart    = "airbb-start";
        private const string SessEnd      = "airbb-end";

        public HomeController(AirBBContext ctx)
        {
            _context = ctx;
        }

        // GET: /
        public IActionResult Index(
            int? activeLocationId, int? guests, DateTime? startDate, DateTime? endDate, string? clear)
        {
            // treat "true", "1", "on" as true
            bool isClear = !string.IsNullOrEmpty(clear) &&
                        (clear.Equals("true", StringComparison.OrdinalIgnoreCase)
                            || clear == "1" || clear.Equals("on", StringComparison.OrdinalIgnoreCase));

            if (isClear)
            {
                HttpContext.Session.Remove(SessLocation);
                HttpContext.Session.Remove(SessGuests);
                HttpContext.Session.Remove(SessStart);
                HttpContext.Session.Remove(SessEnd);
                return RedirectToAction(nameof(Index));   // clean URL, no query
            }

            // 1) read previous values from session
            int? storedLocationId = HttpContext.Session.GetInt32(SessLocation);
            int? storedGuests     = HttpContext.Session.GetInt32(SessGuests);
            string? storedStart   = HttpContext.Session.GetString(SessStart);
            string? storedEnd     = HttpContext.Session.GetString(SessEnd);

            // 2) decide what to use (query wins over session)
            int? finalLocationId = activeLocationId ?? storedLocationId;
            int finalGuests = guests ?? storedGuests ?? 1;

            DateTime? finalStart = startDate;
            if (!finalStart.HasValue && !string.IsNullOrEmpty(storedStart))
            {
                if (DateTime.TryParseExact(storedStart, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedStart))
                {
                    finalStart = parsedStart;
                }
            }

            DateTime? finalEnd = endDate;
            if (!finalEnd.HasValue && !string.IsNullOrEmpty(storedEnd))
            {
                if (DateTime.TryParseExact(storedEnd, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedEnd))
                {
                    finalEnd = parsedEnd;
                }
            }

            // 3) write back to session (only primitives/strings)
            if (finalLocationId.HasValue)
                HttpContext.Session.SetInt32(SessLocation, finalLocationId.Value);
            else
                HttpContext.Session.Remove(SessLocation);

            HttpContext.Session.SetInt32(SessGuests, finalGuests);

            if (finalStart.HasValue)
                HttpContext.Session.SetString(SessStart, finalStart.Value.ToString("yyyy-MM-dd"));
            else
                HttpContext.Session.Remove(SessStart);

            if (finalEnd.HasValue)
                HttpContext.Session.SetString(SessEnd, finalEnd.Value.ToString("yyyy-MM-dd"));
            else
                HttpContext.Session.Remove(SessEnd);

            // 4) load dropdown data
            var locations = _context.Locations
                                    .OrderBy(l => l.Name)
                                    .ToList();

            // 5) base query + filters
            var residencesQuery = _context.Residences
                                          .Include(r => r.Location)
                                          .AsQueryable();

            if (finalLocationId.HasValue)
                residencesQuery = residencesQuery.Where(r => r.LocationId == finalLocationId.Value);

            if (finalGuests > 1)
                residencesQuery = residencesQuery.Where(r => r.GuestNumber >= finalGuests);

            // (date filter retained in session; apply if your assignment requires)

            var residences = residencesQuery.ToList();

            // 6) build viewmodel
            var vm = new HomeFilterViewModel
            {
                ActiveLocationId = finalLocationId,
                Guests           = finalGuests,
                StartDate        = finalStart,
                EndDate          = finalEnd,
                Locations        = locations,
                Residences       = residences
            };

            return View(vm);
        }

        // POST: /Home/SaveReservation
        [HttpPost]
        public IActionResult SaveReservation(int id)
        {
            // keep it simple: write to cookie for 7 days
            var cookies = new AirBBCookies(Request.Cookies, Response.Cookies);
            var currentIds = cookies.GetReservationIds().ToList();
            if (!currentIds.Contains(id))
                currentIds.Add(id);
            cookies.WriteReservationIds(currentIds);

            TempData["Message"] = "Residence reserved.";
            return RedirectToAction("Index");
        }

        // GET: /Home/Details/101
        [HttpGet]
        public IActionResult Details(int id)
        {
            var residence = _context.Residences
                                    .Include(r => r.Location)
                                    .FirstOrDefault(r => r.ResidenceId == id);

            if (residence == null)
                return NotFound();

            return View(residence);
        }
    }
}