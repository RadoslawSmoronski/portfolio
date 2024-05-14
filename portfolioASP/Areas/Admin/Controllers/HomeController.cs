using Microsoft.AspNetCore.Mvc;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models.ViewModels;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
