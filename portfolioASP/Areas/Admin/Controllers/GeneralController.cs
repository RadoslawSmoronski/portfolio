using Microsoft.AspNetCore.Mvc;
using portfolio.DataAccess.Json;
using portfolio.Models;
using portfolio.Models.ViewModels;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GeneralController : Controller
    {
        AdminGeneralViewModel _viewModel;
        public GeneralController()
        {
            NavbarLogo navbarLogo = JsonFileManager<NavbarLogo>.Get();



            _viewModel = new AdminGeneralViewModel
            {
                NavbarLogo = navbarLogo
            };
        }

        public IActionResult Index()
        {
            return View(_viewModel);
        }
    }
}
