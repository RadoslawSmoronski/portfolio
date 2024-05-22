using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using portfolio.DataAccess.Json;
using portfolio.Models;
using portfolio.Models.Email;
using portfolio.Models.ViewModels;
using portfolio.Utility;
using Microsoft.AspNetCore.Mvc.Localization;
using portfolio.Models.WebsiteTitle;
using portfolio.Models.NavbarLogo;
using portfolio.Models.Footer;

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

            _adminLogin = adminLogin.Value;
            _localizer = localizer;
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

            TempData["success"] = _localizer["EditedWebsiteTitle"].Value;
            return RedirectToAction("Index");
        }

        public IActionResult DeleteWebsiteTitleImage()
        {
            WebsiteTitle obj = JsonFileManager<WebsiteTitle>.Get();

            DeleteImageFile(obj.ImageUrl);

            obj.ImageUrl = null;

            JsonFileManager<WebsiteTitle>.Save(obj);

            TempData["success"] = _localizer["ImageWasRemoved"].Value;
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

            TempData["success"] = _localizer["MenuWasEdited"].Value;
            return RedirectToAction("Index");
        }

        public IActionResult DeleteNavbarImage()
        {
            NavbarLogo navbarLogo = JsonFileManager<NavbarLogo>.Get();

            DeleteImageFile(navbarLogo.ImageUrl);

            navbarLogo.ImageUrl = null;

            JsonFileManager<NavbarLogo>.Save(navbarLogo);

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
