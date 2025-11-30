
using AirBB.Areas.Admin.Models;
using AirBB.Models;
using AirBB.Models.DataLayer;
using AirBB.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LocationsController : Controller
    {
        private readonly AirBBContext _db;
        public LocationsController(AirBBContext db) => _db = db;

        public async Task<IActionResult> Index() =>
            View(await _db.Locations.OrderBy(l => l.City).ToListAsync());

        public IActionResult Create() => View(new AdminLocationViewModel());

        [HttpPost]
        public async Task<IActionResult> Create(AdminLocationViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fix the error.");
                return View(vm);
            }
            var location = new Location
            {
                City = vm.City,
                State = vm.State,
                Country = vm.Country
            };
            _db.Add(location);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var loc = await _db.Locations.FindAsync(id);
            if (loc == null) return NotFound();
            var vm = new AdminLocationViewModel
            {
                LocationId = loc.LocationId,
                City = loc.City,
                State = loc.State,
                Country = loc.Country
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminLocationViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fix the error.");
                return View(vm);
            }
            var loc = await _db.Locations.FindAsync(vm.LocationId);
            if (loc == null) return NotFound();
            loc.City = vm.City;
            loc.State = vm.State;
            loc.Country = vm.Country;
            _db.Update(loc);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var loc = await _db.Locations.FindAsync(id);
            if (loc == null) return NotFound();
            return View(loc);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loc = await _db.Locations.FindAsync(id);
            if (loc != null)
            {
                _db.Remove(loc);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
