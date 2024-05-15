using Microsoft.AspNetCore.Components.Routing;
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
            WebsiteTitle websiteTitle = JsonFileManager<WebsiteTitle>.Get();
            NavbarLogo navbarLogo = JsonFileManager<NavbarLogo>.Get();
            Welcome welcome = JsonFileManager<Welcome>.Get();
            Footer footer = JsonFileManager<Footer>.Get();



            _viewModel = new AdminGeneralViewModel
            {
                WebsiteTitle = websiteTitle,
                NavbarLogo = navbarLogo,
                Welcome = welcome,
                Footer = footer
            };
        }

        public IActionResult Index()
        {
            return View(_viewModel);
        }

        public IActionResult EditWebsiteTitle()
        {
            return View(_viewModel.WebsiteTitle);
        }

        [HttpPost]
        public IActionResult EditWebsiteTitle(WebsiteTitle websiteTitle)
        {
            JsonFileManager<WebsiteTitle>.Save(websiteTitle);

            TempData["success"] = "Edytowałes Tytuł Strony";
            return RedirectToAction("Index", _viewModel);
        }
    }
}
