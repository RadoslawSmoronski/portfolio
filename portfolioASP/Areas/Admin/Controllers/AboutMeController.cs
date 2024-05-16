using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portfolio.DataAccess.Json;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;
using System.Drawing.Drawing2D;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CustomAuthorization]
    public class AboutMeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AboutMeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
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

                TempData["success"] = "Edytowałes O Mnie";
                return RedirectToAction("Index");

            }

            return View(aboutMe);
        }

    }
}
