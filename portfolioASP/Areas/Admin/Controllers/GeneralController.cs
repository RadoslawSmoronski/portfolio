using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using portfolio.DataAccess.Json;
using portfolio.Models;
using portfolio.Models.ViewModels;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorization]
    public class GeneralController : Controller
    {
        IWebHostEnvironment _webHostEnvironment;
        AdminGeneralViewModel _viewModel;

        public GeneralController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;

            WebsiteTitle websiteTitle = JsonFileManager<WebsiteTitle>.Get();
            NavbarLogo navbarLogo = JsonFileManager<NavbarLogo>.Get();
            Welcome welcome = JsonFileManager<Welcome>.Get();
            Footer footer = JsonFileManager<Footer>.Get();



            _viewModel = new AdminGeneralViewModel
            {
                WebsiteTitle = websiteTitle,
                NavbarLogo = navbarLogo,
                Welcome = welcome,
                Footer = footer,
                EditAdminLogin = new EditAdminLogin()
            };
        }

        public IActionResult Index()
        {
            return View(_viewModel);
        }

        //WebsiteTitle

        public IActionResult EditWebsiteTitle()
        {
            return View(_viewModel.WebsiteTitle);
        }

        [HttpPost]
        public IActionResult EditWebsiteTitle(WebsiteTitle websiteTitle, IFormFile? file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\websiteTitle");

                DeleteImageFile(websiteTitle.ImageUrl);

                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                websiteTitle.ImageUrl = @"\images\websiteTitle\" + fileName;
            }

            JsonFileManager<WebsiteTitle>.Save(websiteTitle);

            TempData["success"] = "Edytowałes Tytuł Strony";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteWebsiteTitleImage()
        {
            WebsiteTitle obj = JsonFileManager<WebsiteTitle>.Get();

            DeleteImageFile(obj.ImageUrl);

            obj.ImageUrl = null;

            JsonFileManager<WebsiteTitle>.Save(obj);

            TempData["success"] = "Zdjęcie zostało usunięte.";
            return RedirectToAction("Index");
        }

        //Navbar

        public IActionResult EditNavbar()
        {
            return View(_viewModel.NavbarLogo);
        }

        [HttpPost]
        public IActionResult EditNavbar(NavbarLogo navbarLogo, IFormFile? file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\navbar");

                DeleteImageFile(navbarLogo.ImageUrl);

                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                navbarLogo.ImageUrl = @"\images\navbar\" + fileName;
            }

            JsonFileManager<NavbarLogo>.Save(navbarLogo);

            TempData["success"] = "Menu zostało edytowane.";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteNavbarImage()
        {
            NavbarLogo navbarLogo = JsonFileManager<NavbarLogo>.Get();

            DeleteImageFile(navbarLogo.ImageUrl);

            navbarLogo.ImageUrl = null;

            JsonFileManager<NavbarLogo>.Save(navbarLogo);

            TempData["success"] = "Zdjęcie zostało usunięte.";
            return RedirectToAction("Index");
        }

        private void DeleteImageFile(string? imgUrl)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            if (imgUrl != null)
            {
                var oldImagePath =
                    Path.Combine(wwwRootPath, imgUrl.TrimStart('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
        }

        //Welcome

        public IActionResult EditWelcome()
        {
            return View(_viewModel.Welcome);
        }

        [HttpPost]
        public IActionResult EditWelcome(Welcome welcome)
        {
            JsonFileManager<Welcome>.Save(welcome);

            TempData["success"] = "Edytowałes sekecji Witam";
            return RedirectToAction("Index");
        }

        //Footer

        public IActionResult EditFooter()
        {
            return View(_viewModel.Footer);
        }

        [HttpPost]
        public IActionResult EditFooter(Footer footer)
        {
            JsonFileManager<Footer>.Save(footer);

            TempData["success"] = "Edytowałes sekecji Stopka";
            return RedirectToAction("Index");
        }

        //EditPassword

        public IActionResult EditPassword()
        {
            return View(_viewModel.EditAdminLogin);
        }

        [HttpPost]
        public IActionResult EditPassword(EditAdminLogin editAdminLogin)
        {
            return View(_viewModel.EditAdminLogin);
        }
    }
}
