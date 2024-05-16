using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;
using portfolio.Models.Email;
using portfolio.Models.ViewModels;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly AdminLogin _adminLogin;

        public HomeController(IOptions<AdminLogin> adminLogin)
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
            if(BCrypt.Net.BCrypt.Verify(adminLogin.Password, _adminLogin.Password))
            {
                HttpContext.Session.SetString("IsActiveSession", "true");
                return View("Index");

            }

            return View();
        }

    }
}
