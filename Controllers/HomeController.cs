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
            return View();
        }

        public IActionResult CancellationPolicy()
        {
            return View();
        }

        public IActionResult Terms()
        {
            return View();
        }

        public IActionResult CookiePolicy()
        {
            return View();
        }
    }
}