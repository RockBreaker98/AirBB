using AirBB.Areas.Admin.Models;
using AirBB.Models.DomainModels;
using AirBB.Models.DataLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using AirBB.Models.DataLayer;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ResidencesController : Controller
    {
        private readonly IRepository<Residence> residenceData;
        private readonly IRepository<Location> locationData;
        private readonly IRepository<Client> clientData;

        public ResidencesController(
            IRepository<Residence> resRepo,
            IRepository<Location> locRepo,
            IRepository<Client> clientRepo)
        {
            residenceData = resRepo;
            locationData = locRepo;
            clientData = clientRepo;
        }

        // ---------------- Helper methods ----------------

        private void PopulateLocations()
        {
            var options = new QueryOptions<Location>
            {
                OrderBy = l => l.City
            };

            var locations = locationData.List(options);

            ViewBag.Locations = new SelectList(
                locations,
                nameof(Location.LocationId),
                nameof(Location.City)
            );
        }

        private void PopulateOwners()
        {
            var options = new QueryOptions<Client>
            {
                Where = c => c.UserType == UserType.Owner,
                OrderBy = c => c.Name
            };

            var owners = clientData.List(options);

            ViewBag.Owners = new SelectList(
                owners,
                nameof(Client.ClientId),
                nameof(Client.Name)
            );
        }

        private bool IsValidOwner(int ownerId)
        {
            var options = new QueryOptions<Client>
            {
                Where = c => c.ClientId == ownerId && c.UserType == UserType.Owner
            };

            return clientData.List(options).Any();
        }

        // ---------------- Actions ----------------

        // GET: /Admin/Residences
        public IActionResult Index()
        {
            var options = new QueryOptions<Residence>
            {
                OrderBy = r => r.Name
            };
            options.Include("Location");

            var residences = residenceData.List(options);
            return View(residences);
        }

        // GET: /Admin/Residences/Create
        public IActionResult Create()
        {
            PopulateLocations();
            PopulateOwners();

            return View(new AdminResidenceViewModel());
        }

        // POST: /Admin/Residences/Create
        [HttpPost]
        public IActionResult Create(AdminResidenceViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fix the error.");
                PopulateLocations();
                PopulateOwners();
                return View(vm);
            }

            // server-side owner validation
            if (!IsValidOwner(vm.OwnerId))
            {
                ModelState.AddModelError(nameof(vm.OwnerId),
                    "OwnerId must exist and be an Owner.");
                PopulateLocations();
                PopulateOwners();
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
                Price         = vm.Price,
                PricePerNight = vm.Price  // keep client cards happy
            };

            residenceData.Insert(res);
            residenceData.Save();

            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Residences/Edit/{id}
        public IActionResult Edit(int id)
        {
            var res = residenceData.Get(id);
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
            PopulateOwners();

            return View(vm);
        }

        // POST: /Admin/Residences/Edit
        [HttpPost]
        public IActionResult Edit(AdminResidenceViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fix the error.");
                PopulateLocations();
                PopulateOwners();
                return View(vm);
            }

            if (!IsValidOwner(vm.OwnerId))
            {
                ModelState.AddModelError(nameof(vm.OwnerId),
                    "OwnerId must exist and be an Owner.");
                PopulateLocations();
                PopulateOwners();
                return View(vm);
            }

            var res = residenceData.Get(vm.ResidenceId);
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
            res.PricePerNight = vm.Price;

            residenceData.Update(res);
            residenceData.Save();

            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Residences/Delete/{id}
        public IActionResult Delete(int id)
        {
            var options = new QueryOptions<Residence>
            {
                Where = r => r.ResidenceId == id
            };
            options.Include("Location");

            var res = residenceData.Get(options);
            if (res == null) return NotFound();

            return View(res);
        }

        // POST: /Admin/Residences/Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var res = residenceData.Get(id);
            if (res != null)
            {
                residenceData.Delete(res);
                residenceData.Save();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}