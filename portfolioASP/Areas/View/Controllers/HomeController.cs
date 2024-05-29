using Microsoft.AspNetCore.Mvc;
using portfolio.DataAccess.Json;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;
using portfolio.Models.Email;
using portfolio.Models.ViewModels;
using portfolio.Utility.Email;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Localization;
using portfolio.Models.AboutMe;
using System.Globalization;
using portfolio.Models.WebsiteTitle;
using portfolio.Models.Navbar;
using portfolio.Models.Footer;
using portfolio.Models.Welcome;
using Microsoft.AspNetCore.Localization;

namespace portfolioASP.Areas.View.Controllers
{
    [Area("View")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ViewHomePageViewModel _model;
        private readonly IHtmlLocalizer<HomeController> _localizer;
        private readonly string currentUICulture = CultureInfo.CurrentUICulture.Name;

        public HomeController(IUnitOfWork unitOfWork, IEmailService emailService, IHtmlLocalizer<HomeController> localizer)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;

            _model = new ViewHomePageViewModel
            {
                WelcomeView = new WelcomeView(JsonFileManager<Welcome>.Get(), currentUICulture),
                AboutMeView = new AboutMeView(JsonFileManager<AboutMe>.Get(), currentUICulture),
                SkillViews = _unitOfWork.SkillRepository.GetAllView(currentUICulture),
                ProjectViews = _unitOfWork.ProjectRepository.GetAllView(currentUICulture),
                Contacts = _unitOfWork.ContactRepository.GetAll().ToList()
            };

            _localizer = localizer;
        }

        public IActionResult Index()
        {
            WebsiteTitleView websiteTitleView = new WebsiteTitleView(JsonFileManager<WebsiteTitle>.Get(), currentUICulture);
            ViewData["WebsiteTitleView"] = websiteTitleView;

            NavbarView navbarView = new NavbarView(JsonFileManager<Navbar>.Get(), currentUICulture);
            ViewData["NavbarView"] = navbarView;

            FooterView footerView = new FooterView(JsonFileManager<Footer>.Get(), currentUICulture);
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


                AutoEmailMessageContentView autoMessage = new AutoEmailMessageContentView(JsonFileManager<AutoEmailMessageContent>.Get(), currentUICulture);
                await _emailService.SendEmailAsync(contactForm.Email, autoMessage.Subject, autoMessage.Content);

                var message = new EmailMessage
                {
                    Email = contactForm.Email,
                    Subject = contactForm.Subject,
                    Name = contactForm.Name,
                    Content = contactForm.Content,
                    SentAt = DateTime.Now
                };

                _unitOfWork.EmailMessageRepository.Add(message);
                _unitOfWork.Save();
                TempData["success"] = _localizer["MessageWasSent"].Value;
            }
            catch(Exception ex)
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
