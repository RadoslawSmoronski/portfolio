using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using portfolio.DataAccess.Json;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;
using portfolio.Models.Email;
using portfolio.Models.ViewModels;
using portfolio.Utility.Email;
using System.Drawing.Drawing2D;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmailsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EmailSettings _emailSettings;


        public EmailsController(IUnitOfWork unitOfWork, IOptions<EmailSettings> emailSettings)
        {
            _unitOfWork = unitOfWork;
            _emailSettings = emailSettings.Value;
        }

        public IActionResult Index()
        {
            List<EmailMessage> emailMessages = _unitOfWork.EmailMessageRepository.GetAll().ToList();

            return View(emailMessages);
        }

        public IActionResult EmailConfigure()
        {
            AdminEmailsEmailConfigureDetailsPageViewModel viewModel = new AdminEmailsEmailConfigureDetailsPageViewModel();
            viewModel.EmailMessageContent = JsonFileManager<AutoEmailMessageContent>.Get();
            viewModel.EmailSettings = _emailSettings;

            return View("EmailConfigure/Details", viewModel);
        }

        public IActionResult EmailEdit()
        {
            return View("EmailConfigure/EmailEdit", _emailSettings);
        }

        [HttpPost]
        public IActionResult EmailEdit(EmailSettings emailSettings)
        {

            try
            {
                emailSettings.CheckConnection();

                TempData["success"] = "Dane zostały zmienione pomyślnie.";
                return RedirectToAction("EmailConfigure");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View(_emailSettings);
            }
        }

    }
}
