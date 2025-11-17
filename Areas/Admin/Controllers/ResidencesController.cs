using AirBB.Areas.Admin.Models;
using AirBB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ResidencesController : Controller
    {
        private readonly AirBBContext _context;
        public ResidencesController(AirBBContext context) => _context = context;

        // helper to populate location dropdown (City shown like LocationsController uses)
        private void PopulateLocations()
        {
            ViewBag.Locations = new SelectList(
                _context.Locations.OrderBy(l => l.City),
                nameof(Location.LocationId),
                nameof(Location.City)
            );
        }

        // GET: /Admin/Residences
        public async Task<IActionResult> Index() =>
            View(await _context.Residences
                               .Include(r => r.Location)
                               .OrderBy(r => r.Name)
                               .ToListAsync());

        // GET: /Admin/Residences/Create
        public IActionResult Create()
        {
            PopulateLocations();
            return View(new AdminResidenceViewModel());
        }

        // POST: /Admin/Residences/Create
        [HttpPost]
        public async Task<IActionResult> Create(AdminResidenceViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fix the error.");
                PopulateLocations();
                return View(vm);
            }

            var res = new Residence
            {
                Name          = vm.Name,
                LocationId    = vm.LocationId,
                OwnerId       = vm.OwnerId,
                Accommodation = vm.Accommodation,
                Bedrooms      = vm.Bedrooms,
                Bathrooms     = vm.Bathrooms,
                BuiltYear     = vm.BuiltYear,
                Image         = vm.Image,
                Price         = vm.Price
            };

            _context.Residences.Add(res);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Residences/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var res = await _context.Residences.FindAsync(id);
            if (res == null) return NotFound();

            var vm = new AdminResidenceViewModel
            {
                ResidenceId   = res.ResidenceId,
                Name          = res.Name,
                LocationId    = res.LocationId,
                OwnerId       = res.OwnerId,
                Accommodation = res.Accommodation,
                Bedrooms      = res.Bedrooms,
                Bathrooms     = res.Bathrooms,
                BuiltYear     = res.BuiltYear,
                Image         = res.Image,
                Price         = res.Price
            };

            PopulateLocations();
            return View(vm);
        }

        // POST: /Admin/Residences/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(AdminResidenceViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fix the error.");
                PopulateLocations();
                return View(vm);
            }

            var res = await _context.Residences.FindAsync(vm.ResidenceId);
            if (res == null) return NotFound();

            res.Name          = vm.Name;
            res.LocationId    = vm.LocationId;
            res.OwnerId       = vm.OwnerId;
            res.Accommodation = vm.Accommodation;
            res.Bedrooms      = vm.Bedrooms;
            res.Bathrooms     = vm.Bathrooms;
            res.BuiltYear     = vm.BuiltYear;
            res.Image         = vm.Image;
            res.Price         = vm.Price;

            _context.Update(res);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Residences/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _context.Residences
                                    .Include(r => r.Location)
                                    .FirstOrDefaultAsync(r => r.ResidenceId == id);
            if (res == null) return NotFound();
            return View(res);
        }

        // POST: /Admin/Residences/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var res = await _context.Residences.FindAsync(id);
            if (res != null)
            {
                _context.Remove(res);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}