using Microsoft.AspNetCore.Mvc;

namespace AirBB.Controllers
{
    public class ResidenceController : Controller
    {
        public IActionResult List(string id = "All")
        {
            return Content($"Public Area - ResidenceController - List Action - id={id}");
        }
    }
}
