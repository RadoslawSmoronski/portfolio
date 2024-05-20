using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using portfolio.DataAccess.Json;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;
using System.Drawing.Drawing2D;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorization]
    public class AboutMeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHtmlLocalizer<AboutMeController> _localizer;

        public AboutMeController(IWebHostEnvironment webHostEnvironment, IHtmlLocalizer<AboutMeController> localizer)
        {
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            return View(JsonFileManager<AboutMe>.Get());
        }

        public IActionResult Edit()
        {
            return View(JsonFileManager<AboutMe>.Get());
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
                    string productPath = Path.Combine(wwwRootPath, @"images\aboutme");

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

                    aboutMe.ImageUrl = @"\images\aboutme\" + fileName;
                }

                JsonFileManager<AboutMe>.Save(aboutMe);

                TempData["success"] = _localizer["AboutMeWasEdited"].Value;
                return RedirectToAction("Index");

            }

            return View(aboutMe);
        }

    }
}
