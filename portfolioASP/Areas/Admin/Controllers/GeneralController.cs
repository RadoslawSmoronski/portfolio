using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using portfolio.DataAccess.Json;
using portfolio.Models;
using portfolio.Models.Email;
using portfolio.Models.ViewModels;
using portfolio.Utility;
using Microsoft.AspNetCore.Mvc.Localization;
using portfolio.Models.WebsiteTab;
using portfolio.Models.Navbar;
using portfolio.Models.Footer;
using portfolio.Models.Welcome;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorization]
    public class GeneralController : Controller
    {
        IWebHostEnvironment _webHostEnvironment;
        AdminGeneralViewModel _viewModel;
        private readonly AdminLogin _adminLogin;
        private readonly IHtmlLocalizer<GeneralController> _localizer;

        public GeneralController(IWebHostEnvironment webHostEnvironment, IOptionsSnapshot<AdminLogin> adminLogin, IHtmlLocalizer<GeneralController> localizer)
        {
            _webHostEnvironment = webHostEnvironment;

            WebsiteTab websiteTab = JsonFileManager<WebsiteTab>.Get();
            Navbar navbar = JsonFileManager<Navbar>.Get();
            Welcome welcome = JsonFileManager<Welcome>.Get();
            Footer footer = JsonFileManager<Footer>.Get();

            _viewModel = new AdminGeneralViewModel
            {
                WebsiteTab = websiteTab,
                Navbar = navbar,
                Welcome = welcome,
                Footer = footer,
                EditAdminLogin = new EditAdminLogin()
            };

            _adminLogin = adminLogin.Value;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            return View(_viewModel);
        }

        //WebsiteTab

        public IActionResult EditWebsiteTab()
        {
            return View(_viewModel.WebsiteTab);
        }

        [HttpPost]
        public IActionResult EditWebsiteTab(WebsiteTab websiteTab, IFormFile? file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\websiteTab");

                DeleteImageFile(websiteTab.ImageUrl);

                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                websiteTab.ImageUrl = @"\images\websiteTab\" + fileName;
            }

            JsonFileManager<WebsiteTab>.Save(websiteTab);

            TempData["success"] = _localizer["EditedWebsiteTab"].Value;
            return RedirectToAction("Index");
        }

        public IActionResult DeleteWebsiteTabImage()
        {
            WebsiteTab obj = JsonFileManager<WebsiteTab>.Get();

            DeleteImageFile(obj.ImageUrl);

            obj.ImageUrl = null;

            JsonFileManager<WebsiteTab>.Save(obj);

            TempData["success"] = _localizer["ImageWasRemoved"].Value;
            return RedirectToAction("Index");
        }

        //Navbar

        public IActionResult EditNavbar()
        {
            return View(_viewModel.Navbar);
        }

        [HttpPost]
        public IActionResult EditNavbar(Navbar navbar, IFormFile? file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\navbar");

                DeleteImageFile(navbar.ImageUrl);

                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                navbar.ImageUrl = @"\images\navbar\" + fileName;
            }

            JsonFileManager<Navbar>.Save(navbar);

            TempData["success"] = _localizer["MenuWasEdited"].Value;
            return RedirectToAction("Index");
        }

        public IActionResult DeleteNavbarImage()
        {
            Navbar navbar = JsonFileManager<Navbar>.Get();

            DeleteImageFile(navbar.ImageUrl);

            navbar.ImageUrl = null;

            JsonFileManager<Navbar>.Save(navbar);

            TempData["success"] = _localizer["ImageWasRemoved"].Value;
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

            TempData["success"] = _localizer["WelcomeSectionWasEdited"].Value;
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

            TempData["success"] = _localizer["FooterWasEdited"].Value;
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
            if (ModelState.IsValid)
            {
                if (BCrypt.Net.BCrypt.Verify(editAdminLogin.Password, _adminLogin.Password))
                {
                    string newHashedPassword = BCrypt.Net.BCrypt.HashPassword(editAdminLogin.NewPassword);

                    EditAppSettings.AddOrUpdateAppSetting<String>("AdminLogin:Password", newHashedPassword);
                    TempData["success"] = _localizer["PasswordWasChanged"].Value;
                    return RedirectToAction("Index");

                }
            }

            TempData["error"] = _localizer["WrongCurrentPassword"].Value;

            return View(_viewModel.EditAdminLogin);
        }
    }
}
