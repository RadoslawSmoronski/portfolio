using Microsoft.AspNetCore.Mvc;
using portfolio.DataAccess.Json;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;
using portfolio.Models.Email;
using portfolio.Models.ViewModels;
using portfolio.Utility.Email;
using System.Diagnostics;

namespace portfolioASP.Areas.View.Controllers
{
    [Area("View")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ViewHomePageViewModel _model;

        public HomeController(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;

            _model = new ViewHomePageViewModel
            {
                Welcome = JsonFileManager<Welcome>.Get(),
                AboutMe = JsonFileManager<AboutMe>.Get(),
                Skills = _unitOfWork.SkillRepository.GetAll().ToList(),
                Projects = _unitOfWork.ProjectRepository.GetAll().ToList(),
                Contacts = _unitOfWork.ContactRepository.GetAll().ToList(),
            };
        }

        public IActionResult Index()
        {
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
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
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
                TempData["success"] = "Wiadomość została wysłana.";
            }

            return RedirectToAction("Index", _model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
