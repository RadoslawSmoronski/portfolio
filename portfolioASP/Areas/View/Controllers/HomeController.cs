using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using portfolio.DataAccess.Data;
using portfolio.DataAccess.Json;
using portfolio.Models;
using portfolio.Models.AboutMe;
using portfolio.Models.Email;
using portfolio.Models.Footer;
using portfolio.Models.Navbar;
using portfolio.Models.Project;
using portfolio.Models.Skill;
using portfolio.Models.ViewModels;
using portfolio.Models.WebsiteTab;
using portfolio.Models.Welcome;
using portfolio.Utility.Email;
using System.Diagnostics;
using System.Globalization;

namespace portfolioASP.Areas.View.Controllers
{
    [Area("View")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IEmailService _emailService;
        private readonly ViewHomePageViewModel _model;
        private readonly IHtmlLocalizer<HomeController> _localizer;
        private readonly string currentUICulture = CultureInfo.CurrentUICulture.Name;
        private readonly IJsonFileManager _jsonFileManager;

        public HomeController(ApplicationDbContext dbContext,
            IEmailService emailService,
            IHtmlLocalizer<HomeController> localizer,
            IJsonFileManager jsonFileManager)
        {
            _dbContext = dbContext;
            _emailService = emailService;
            _localizer = localizer;
            _jsonFileManager = jsonFileManager;

            _model = new ViewHomePageViewModel
            {
                WelcomeView = new WelcomeView(_jsonFileManager.Get<Welcome>(), currentUICulture),
                AboutMeView = new AboutMeView(_jsonFileManager.Get<AboutMe>(), currentUICulture),
                SkillViews = _dbContext.GetListView<Skill, SkillView>(obj => new SkillView(obj, currentUICulture)),
                ProjectViews = _dbContext.GetListView<Project, ProjectView>(obj => new ProjectView(obj, currentUICulture)),
                Contacts = _dbContext.Contacts.ToList()
            };
        }

        public IActionResult Index()
        {
            WebsiteTabView websiteTabView = new WebsiteTabView(_jsonFileManager.Get<WebsiteTab>(), currentUICulture);
            ViewData["WebsiteTabView"] = websiteTabView;

            NavbarView navbarView = new NavbarView(_jsonFileManager.Get<Navbar>(), currentUICulture);
            ViewData["NavbarView"] = navbarView;

            FooterView footerView = new FooterView(_jsonFileManager.Get<Footer>(), currentUICulture);
            ViewData["FooterView"] = footerView;

            return View(_model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactForm contactForm)
        {
            if(contactForm == null)
            {
                return NotFound();
            }

            try
            {


                AutoEmailMessageContentView autoMessage = new AutoEmailMessageContentView(_jsonFileManager.Get<AutoEmailMessageContent>(), currentUICulture);
                await _emailService.SendEmailAsync(contactForm.Email, autoMessage.Subject, autoMessage.Content);

                DateTime localDateTime = DateTime.Now;
                DateTime utcDateTime = localDateTime.ToUniversalTime();

                var message = new EmailMessage
                {
                    Email = contactForm.Email,
                    Subject = contactForm.Subject,
                    Name = contactForm.Name,
                    Content = contactForm.Content,
                    SentAt = utcDateTime
                };

                _dbContext.EmailMessages.Add(message);
                _dbContext.SaveChanges();
                TempData["success"] = _localizer["MessageWasSent"].Value;
            }
            catch (DbUpdateException ex)
            {
                TempData["error"] = ex.InnerException?.Message;
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["error"] = _localizer["MessageSendError"].Value;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
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

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
