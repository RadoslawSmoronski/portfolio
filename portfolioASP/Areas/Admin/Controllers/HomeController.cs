using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;
using portfolio.Models.Email;
using portfolio.Models.ViewModels;
using portfolio.Utility;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly AdminLogin _adminLogin;
        private readonly IHtmlLocalizer<HomeController> _localizer;

        public HomeController(IOptionsSnapshot<AdminLogin> adminLogin, IHtmlLocalizer<HomeController> localizer)
        {
            _adminLogin = adminLogin.Value;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            //HttpContext.Session.SetString("IsActiveSession", "true");

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
                TempData["error"] = _localizer["ToMuchFailedLoginAttempt"].Value;
                return View();
            }

            if (ModelState.IsValid)
            {
                if (BCrypt.Net.BCrypt.Verify(adminLogin.Password, _adminLogin.Password))
                {
                    AdminLoginFailedBanned.RemoveFailderLoginAttempts(ipAddress);
                    HttpContext.Session.SetString("IsActiveSession", "true");
                    TempData["success"] = _localizer["SuccessfullyLoggedIn"].Value;
                    return View("Index");

                }
            }

            AdminLoginFailedBanned.AddFailedLoginAttempts(ipAddress);

            Task.Delay(1000).Wait();
            TempData["error"] = _localizer["PasswordIsNotCorrect"].Value;
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.SetString("IsActiveSession", "false");

            TempData["success"] = _localizer["Logout"].Value;
            return RedirectToAction("Index", new { area = "View" });
        }

        public IActionResult ChangeLanguage(string lang)
        {
            if (!string.IsNullOrEmpty(lang))
            {
                var culture = new CultureInfo(lang);
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }

    }
}
