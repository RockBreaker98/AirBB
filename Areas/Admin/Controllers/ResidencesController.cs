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

        // ----------------------------
        // HELPERS
        // ----------------------------

        private void PopulateLocations()
        {
            ViewBag.Locations = new SelectList(
                _context.Locations.OrderBy(l => l.City),
                nameof(Location.LocationId),
                nameof(Location.City)
            );
        }

        private Residence MapToEntity(AdminResidenceViewModel vm) =>
            new Residence
            {
                ResidenceId   = vm.ResidenceId,
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

        private AdminResidenceViewModel MapToViewModel(Residence r) =>
            new AdminResidenceViewModel
            {
                ResidenceId   = r.ResidenceId,
                Name          = r.Name,
                LocationId    = r.LocationId,
                OwnerId       = r.OwnerId,
                Accommodation = r.Accommodation,
                Bedrooms      = r.Bedrooms,
                Bathrooms     = r.Bathrooms,
                BuiltYear     = r.BuiltYear,
                Image         = r.Image,
                Price         = r.Price
            };

        // ----------------------------
        // LIST
        // ----------------------------

        public async Task<IActionResult> Index() =>
            View(await _context.Residences
                .Include(r => r.Location)
                .OrderBy(r => r.Name)
                .ToListAsync());

        // ----------------------------
        // CREATE - PAGE 1 (FORM)
        // ----------------------------

        public IActionResult Create()
        {
            PopulateLocations();
            return View(new AdminResidenceViewModel());
        }

        // ----------------------------
        // CREATE - PAGE 2 (CONFIRMATION)
        // ----------------------------

        [HttpPost]
        public IActionResult Review(AdminResidenceViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                PopulateLocations();
                return View("Create", vm);
            }

            return View("Confirm", vm);
        }

        // ----------------------------
        // CREATE - PAGE 3 (SAVE)
        // ----------------------------

        [HttpPost]
        public async Task<IActionResult> CreateConfirmed(AdminResidenceViewModel vm)
        {
            var res = MapToEntity(vm);

            _context.Residences.Add(res);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ----------------------------
        // EDIT
        // ----------------------------

        public async Task<IActionResult> Edit(int id)
        {
            var res = await _context.Residences.FindAsync(id);
            if (res == null) return NotFound();

            var vm = MapToViewModel(res);

            PopulateLocations();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminResidenceViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                PopulateLocations();
                return View(vm);
            }

            var entity = await _context.Residences.FindAsync(vm.ResidenceId);
            if (entity == null) return NotFound();

            // update mapped fields
            entity.Name          = vm.Name;
            entity.LocationId    = vm.LocationId;
            entity.OwnerId       = vm.OwnerId;
            entity.Accommodation = vm.Accommodation;
            entity.Bedrooms      = vm.Bedrooms;
            entity.Bathrooms     = vm.Bathrooms;
            entity.BuiltYear     = vm.BuiltYear;
            entity.Image         = vm.Image;
            entity.Price         = vm.Price;

            _context.Update(entity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ----------------------------
        // DELETE
        // ----------------------------

        public async Task<IActionResult> Delete(int id)
        {
            var res = await _context.Residences
                .Include(r => r.Location)
                .FirstOrDefaultAsync(r => r.ResidenceId == id);

            if (res == null) return NotFound();

            return View(res);
        }

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
