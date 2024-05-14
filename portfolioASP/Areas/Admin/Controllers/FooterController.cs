using Azure;
using Microsoft.AspNetCore.Mvc;
using portfolio.DataAccess.Json;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;
using System.Drawing.Drawing2D;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FooterController : Controller
    {
        public IActionResult Index()
        {
            return View(JsonFileManager<Footer>.Get());
        }

        public IActionResult Edit()
        {
            return View(JsonFileManager<Footer>.Get());
        }

        [HttpPost]
        public IActionResult Edit(Footer footer)
        {
            JsonFileManager<Footer>.Save(footer);

            TempData["success"] = "Edytowałes Stopkę";
            return RedirectToAction("Index", footer);
        }

    }
}
