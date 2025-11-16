using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirBB.Models;
using AirBB.Areas.Admin.Validations;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly AirBBContext _context;

        public UsersController(AirBBContext context)
        {
            _context = context;
        }

        // GET: /Admin/Users
        public IActionResult Index()
        {
            var users = _context.Clients.ToList();
            return View(users);
        }

        // GET: /Admin/Users/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Users/Create
        [HttpPost]
        public IActionResult Create(Client model)
        {
            if (!ModelState.IsValid) return View(model);

            // Uniqueness checks
            var emailMsg = Check.EmailExists(_context, model.Email);
            var phoneMsg = Check.PhoneExists(_context, model.PhoneNumber);

            if (!string.IsNullOrEmpty(emailMsg))
                ModelState.AddModelError(nameof(model.Email), emailMsg);
            if (!string.IsNullOrEmpty(phoneMsg))
                ModelState.AddModelError(nameof(model.PhoneNumber), phoneMsg);

            if (!ModelState.IsValid) return View(model);

            _context.Clients.Add(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Users/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _context.Clients.FirstOrDefault(u => u.ClientId == id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: /Admin/Users/Edit/5
        [HttpPost]
        public IActionResult Edit(Client model)
        {
            if (!ModelState.IsValid) return View(model);

            var emailMsg = Check.EmailExists(_context, model.Email, model.ClientId);
            var phoneMsg = Check.PhoneExists(_context, model.PhoneNumber, model.ClientId);

            if (!string.IsNullOrEmpty(emailMsg))
                ModelState.AddModelError(nameof(model.Email), emailMsg);
            if (!string.IsNullOrEmpty(phoneMsg))
                ModelState.AddModelError(nameof(model.PhoneNumber), phoneMsg);

            if (!ModelState.IsValid) return View(model);

            _context.Update(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Users/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var user = _context.Clients.FirstOrDefault(u => u.ClientId == id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: /Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")] 
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _context.Clients.Find(id);
            if (user != null)
            {
                _context.Clients.Remove(user);
                _context.SaveChanges();
            }

            TempData["SuccessMessage"] = "User deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult ValidateOwnerId(int? ownerId, int? OwnerId)
        {
            var id = ownerId ?? OwnerId;

            if (id == null) return Json(true);
            var exists = _context.Clients.Any(c => c.ClientId == id.Value);

            return Json(exists);
        }
    }
}