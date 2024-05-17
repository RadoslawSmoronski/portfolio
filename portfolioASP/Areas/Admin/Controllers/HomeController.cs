using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;
using portfolio.Models.Email;
using portfolio.Models.ViewModels;
using portfolio.Utility;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly AdminLogin _adminLogin;

        public HomeController(IOptionsSnapshot<AdminLogin> adminLogin)
        {
            _adminLogin = adminLogin.Value;
        }

        public IActionResult Index()
        {
            var isActiveSession = HttpContext.Session.GetString("IsActiveSession");

            if (isActiveSession == "true")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public IActionResult Login()
        {
            var isActiveSession = HttpContext.Session.GetString("IsActiveSession");

            if (isActiveSession != "true")
            {
                AdminLogin adminLogin = new AdminLogin();

                return View(adminLogin);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Login(AdminLogin adminLogin)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

            if(AdminLoginFailedBanned.IsUserBanned(ipAddress))
            {
                TempData["error"] = "Przekroczyleś ilość prób, spróbuj później.";
                return View();
            }

            if (ModelState.IsValid)
            {
                if (BCrypt.Net.BCrypt.Verify(adminLogin.Password, _adminLogin.Password))
                {
                    AdminLoginFailedBanned.RemoveFailderLoginAttempts(ipAddress);
                    HttpContext.Session.SetString("IsActiveSession", "true");
                    TempData["success"] = "Zalogowano pomyslnie.";
                    return View("Index");

                }
            }

            AdminLoginFailedBanned.AddFailedLoginAttempts(ipAddress);

            Task.Delay(1000).Wait();
            TempData["error"] = "Nieprawidłowe hasło.";
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.SetString("IsActiveSession", "false");

            TempData["success"] = "Wylogowano";
            return RedirectToAction("Index", new { area = "View" });
        }

    }
}
