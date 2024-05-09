using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using portfolio.DataAccess.Json;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;
using portfolio.Utility.Email;
using System.Drawing.Drawing2D;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmailsController : Controller
    {
        private readonly EmailSettings _emailSettings;

        public EmailsController(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public IActionResult Index()
        {
            return View(_emailSettings);
        }

        public IActionResult Edit()
        {
            return View(_emailSettings);
        }

        [HttpPost]
        public IActionResult Edit(EmailSettings emailSettings)
        {

            try
            {
                emailSettings.CheckConnection();

                TempData["success"] = "Dane zostały zmienione pomyślnie.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View(_emailSettings);
            }
        }

    }
}
