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
using portfolio.DataAccess.Data;
using portfolio.Models.ConfigureData;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorization]
    public class GeneralController : Controller
    {
        IWebHostEnvironment _webHostEnvironment;
        AdminGeneralViewModel _viewModel;
        private readonly IHtmlLocalizer<GeneralController> _localizer;
        private readonly IJsonFileManager _jsonFileManager;
        private readonly ApplicationDbContext _dbContext;

        public GeneralController(IWebHostEnvironment webHostEnvironment,
            IHtmlLocalizer<GeneralController> localizer,
            IJsonFileManager jsonFileManager,
            ApplicationDbContext dbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
            _jsonFileManager = jsonFileManager;
            _dbContext = dbContext;

            WebsiteTab websiteTab = _jsonFileManager.Get<WebsiteTab>();
            Navbar navbar = _jsonFileManager.Get<Navbar>();
            Welcome welcome = _jsonFileManager.Get<Welcome>();
            Footer footer = _jsonFileManager.Get<Footer>();

            _viewModel = new AdminGeneralViewModel
            {
                WebsiteTab = websiteTab,
                Navbar = navbar,
                Welcome = welcome,
                Footer = footer,
                EditAdminLogin = new EditAdminLogin()
            };
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

            _jsonFileManager.Save<WebsiteTab>(websiteTab);

            TempData["success"] = _localizer["EditedWebsiteTab"].Value;
            return RedirectToAction("Index");
        }

        public IActionResult DeleteWebsiteTabImage()
        {
            WebsiteTab obj = _jsonFileManager.Get<WebsiteTab>();

            DeleteImageFile(obj.ImageUrl);

            obj.ImageUrl = null;

            _jsonFileManager.Save<WebsiteTab>(obj);

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

            _jsonFileManager.Save<Navbar>(navbar);

            TempData["success"] = _localizer["MenuWasEdited"].Value;
            return RedirectToAction("Index");
        }

        public IActionResult DeleteNavbarImage()
        {
            Navbar navbar = _jsonFileManager.Get<Navbar>();

            DeleteImageFile(navbar.ImageUrl);

            navbar.ImageUrl = null;

            _jsonFileManager.Save<Navbar>(navbar);

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
            _jsonFileManager.Save<Welcome>(welcome);

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
            _jsonFileManager.Save<Footer>(footer);

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
                ConfigureData? configureData = _dbContext.ConfigureDatas.Find(1);
                AdminLogin? adminLoginDB = null;

                if (configureData != null)
                {
                    adminLoginDB = configureData.Convert<AdminLogin>();
                }

                if (configureData != null && adminLoginDB != null && BCrypt.Net.BCrypt.Verify(editAdminLogin.Password, adminLoginDB.Password))
                {
                    string newHashedPassword = BCrypt.Net.BCrypt.HashPassword(editAdminLogin.NewPassword);

                    adminLoginDB.Password = newHashedPassword;
                    configureData.JSON = adminLoginDB.GetJson();
                    _dbContext.SaveChanges();
                   
                    TempData["success"] = _localizer["PasswordWasChanged"].Value;
                    return RedirectToAction("Index");

                }
            }

            TempData["error"] = _localizer["WrongCurrentPassword"].Value;

            return View(_viewModel.EditAdminLogin);
        }
    }
}
