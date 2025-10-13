using Microsoft.AspNetCore.Mvc;

namespace AirBB.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Support()
        {
            return Content("Home controller, Support action");
        }

        public IActionResult CancellationPolicy()
        {
            return Content("Home controller, CancellationPolicy action");
        }

        public IActionResult Terms()
        {
            return Content("Home controller, Terms action");
        }

        public IActionResult CookiePolicy()
        {
            return Content("Home controller, CookiePolicy action");
        }
    }
}