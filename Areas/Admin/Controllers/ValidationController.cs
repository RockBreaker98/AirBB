using Microsoft.AspNetCore.Mvc;
using AirBB.Models;
using AirBB.Models.DataLayer;
using AirBB.Models.DomainModels;
using System.Linq;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ValidationController : Controller
    {
        private readonly AirBBContext _context;

        public ValidationController(AirBBContext context)
        {
            _context = context;
        }

        // Remote validation for OwnerId
        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckOwnerId(int ownerId)
        {
            bool existsAndOwner = _context.Clients
                .Any(c => c.ClientId == ownerId && c.UserType == UserType.Owner);

            if (!existsAndOwner)
            {
                return Json($"OwnerId {ownerId} does not exist or is not an Owner.");
            }

            return Json(true);
        }
    }
}
