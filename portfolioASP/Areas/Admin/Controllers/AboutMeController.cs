using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using portfolio.DataAccess.Json;
using portfolio.Models.AboutMe;
using System.Drawing.Drawing2D;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorization]
    public class AboutMeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHtmlLocalizer<AboutMeController> _localizer;
        private readonly IJsonFileManager _jsonFileManager;

        public AboutMeController(IWebHostEnvironment webHostEnvironment,
            IHtmlLocalizer<AboutMeController> localizer,
            IJsonFileManager jsonFileManager)
        {
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
            _jsonFileManager = jsonFileManager;
        }

        public IActionResult Index()
        {
            return View(_jsonFileManager.Get<AboutMe>());
        }

        public IActionResult Edit()
        {
            return View(_jsonFileManager.Get<AboutMe>());
        }

        [HttpPost]
        public IActionResult Edit(AboutMe aboutMe, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, "images", "aboutme");

                    if (string.IsNullOrEmpty(aboutMe.ImageUrl) == false)
                    {
                        var oldImagePath =
                            Path.Combine(wwwRootPath, aboutMe.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    aboutMe.ImageUrl = Path.Combine("images", "aboutme", fileName);
                }

                _jsonFileManager.Save<AboutMe>(aboutMe);

                TempData["success"] = _localizer["AboutMeWasEdited"].Value;
                return RedirectToAction("Index");

            }

            return View(aboutMe);
        }

    }
}
