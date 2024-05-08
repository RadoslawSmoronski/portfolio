using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using portfolio.DataAccess.Data;
using portfolio.DataAccess.Json;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;
using portfolio.Models.ViewModels;
using portfolio.Utility.Email;
using System.Diagnostics;

namespace portfolioASP.Areas.View.Controllers
{
    [Area("View")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JsonFileManager _jsonFileManager;
        private readonly IEmailService _emailService;
        private readonly EmailSettings _emailSettings;

        public HomeController(
            ILogger<HomeController> logger, IUnitOfWork unitOfWork,
            JsonFileManager jsonFileManager, IEmailService emailService,
            IOptions<EmailSettings> settings)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _jsonFileManager = jsonFileManager;
            _emailService = emailService;
            _emailSettings = settings.Value;
        }

        public IActionResult Index()
        {
            HomePageViewModel model = new HomePageViewModel();

            model.Skills = _unitOfWork.SkillRepository.GetAll().ToList();
            model.Projects = _unitOfWork.ProjectRepository.GetAll().ToList();
            model.Contacts = _unitOfWork.ContactRepository.GetAll().ToList();
            model.AboutMe = _jsonFileManager.AboutMe;


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactForm contactForm)
        {
            if(contactForm == null)
            {
                contactForm = new ContactForm();
            }

            await _emailService.SendEmailAsync("radoslaw.smo@gmail.com", "test subject", "test content");

            HomePageViewModel model = new HomePageViewModel();

            model.Skills = _unitOfWork.SkillRepository.GetAll().ToList();
            model.Projects = _unitOfWork.ProjectRepository.GetAll().ToList();
            model.Contacts = _unitOfWork.ContactRepository.GetAll().ToList();
            model.AboutMe = _jsonFileManager.AboutMe;

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
