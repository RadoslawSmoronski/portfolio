using Azure;
using Microsoft.AspNetCore.Mvc;
using portfolio.DataAccess.Json;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;
using System.Drawing.Drawing2D;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WelcomeController : Controller
    {

        public WelcomeController()
        {
        }

        public IActionResult Index()
        {
            return View(JsonFileManager<Welcome>.Get());
        }

        public IActionResult Edit()
        {
            return View(JsonFileManager<Welcome>.Get());
        }

        [HttpPost]
        public IActionResult Edit(Welcome welcome)
        {
            if (ModelState.IsValid)
            {

                JsonFileManager<Welcome>.Save(welcome);

                TempData["success"] = "Edytowałes sekcjie Witam";
                return RedirectToAction("Index");

            }

            return View(welcome);
        }

    }
}
