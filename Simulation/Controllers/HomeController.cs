using Microsoft.AspNetCore.Mvc;

namespace Stiffiner_Inspection.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
