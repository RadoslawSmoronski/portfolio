using Microsoft.AspNetCore.Mvc;

namespace portfolioASP.Areas.Admin.Controllers
{
    public class GeneralController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
