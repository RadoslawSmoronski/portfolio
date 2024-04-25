using Microsoft.AspNetCore.Mvc;

namespace portfolio.Controllers
{
    public class SkillsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
