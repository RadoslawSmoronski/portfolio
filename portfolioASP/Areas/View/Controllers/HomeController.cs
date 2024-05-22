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

namespace portfolioASP.Areas.View.Controllers
{
    [Area("View")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ViewHomePageViewModel _model;
        private readonly IHtmlLocalizer<HomeController> _localizer;

        public HomeController(IUnitOfWork unitOfWork, IEmailService emailService, IHtmlLocalizer<HomeController> localizer)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;

            string currentUICulture = CultureInfo.CurrentUICulture.Name;

            _model = new ViewHomePageViewModel
            {
                Welcome = JsonFileManager<Welcome>.Get(),
                AboutMeView = new AboutMeView(JsonFileManager<AboutMe>.Get(), currentUICulture),
                SkillViews = _unitOfWork.SkillRepository.GetAllView(currentUICulture),
                ProjectViews = _unitOfWork.ProjectRepository.GetAllView(currentUICulture),
                Contacts = _unitOfWork.ContactRepository.GetAll().ToList(),
            };

            _localizer = localizer;
        }

        public IActionResult Index()
        {
            WebsiteTitle websiteTitle = JsonFileManager<WebsiteTitle>.Get();
            ViewData["WebsiteTitle"] = websiteTitle;

            NavbarLogo navbarLogo = JsonFileManager<NavbarLogo>.Get();
            ViewData["NavbarLogo"] = navbarLogo;

            Footer footer = JsonFileManager<Footer>.Get();
            ViewData["Footer"] = footer;

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
                AutoEmailMessageContent autoMessage =  JsonFileManager<AutoEmailMessageContent>.Get();
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
