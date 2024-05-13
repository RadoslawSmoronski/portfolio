using Microsoft.AspNetCore.Mvc;
using portfolio.DataAccess.Data;
using portfolio.DataAccess.Json;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NavbarLogoController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NavbarLogoController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View(JsonFileManager<NavbarLogo>.Get());
        }

        public IActionResult Edit()
        {
            return View(JsonFileManager<NavbarLogo>.Get());
        }

        [HttpPost]
        public IActionResult Edit(NavbarLogo navbarLogo, IFormFile? file)
        {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\navbar");

                    if (string.IsNullOrEmpty(navbarLogo.ImageUrl) == false)
                    {
                        var oldImagePath =
                            Path.Combine(wwwRootPath, navbarLogo.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    navbarLogo.ImageUrl = @"\images\navbar\" + fileName;
                }

                JsonFileManager<NavbarLogo>.Save(navbarLogo);

                TempData["success"] = "Projekt został utworzony.";
                return RedirectToAction("Index");
        }

    }
}
