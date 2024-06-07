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
using portfolio.DataAccess.Data;
using portfolio.Models.ConfigureData;
using Newtonsoft.Json;
using System;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHtmlLocalizer<HomeController> _localizer;
        private readonly IAdminLoginFailedBanned _adminLoginFailedBanned;

        public HomeController(ApplicationDbContext dbContext,
            IHtmlLocalizer<HomeController> localizer,
            IAdminLoginFailedBanned adminLoginFailedBanned)
        {
            _dbContext = dbContext;
            _localizer = localizer;
            _adminLoginFailedBanned = adminLoginFailedBanned;
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

            if(_adminLoginFailedBanned.IsUserBanned(ipAddress))
            {
                TempData["error"] = _localizer["ToMuchFailedLoginAttempt"].Value;
                return View();
            }

            if (ModelState.IsValid)
            {
                ConfigureData? configureData = _dbContext.ConfigureDatas.Find(1);
                AdminLogin? adminLoginDB = null;

                if (configureData != null)
                {
                    adminLoginDB = configureData.Convert<AdminLogin>();
                }
                
                if (adminLoginDB != null && BCrypt.Net.BCrypt.Verify(adminLogin.Password, adminLoginDB.Password))
                {
                    _adminLoginFailedBanned.RemoveFailedLoginAttempt(ipAddress);
                    HttpContext.Session.SetString("IsActiveSession", "true");
                    TempData["success"] = _localizer["SuccessfullyLoggedIn"].Value;
                    return View("Index");

                }
            }

            _adminLoginFailedBanned.AddFailedLoginAttempt(ipAddress);

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
