using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models.Email;
using portfolio.Models.ViewModels;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EmailSettings _emailSettings;

        public ContactController(IUnitOfWork unitOfWork, IOptions<EmailSettings> emailSettings)
        {
            _unitOfWork = unitOfWork;
            _emailSettings = emailSettings.Value;
        }

        public IActionResult Index()
        {
            List<EmailMessage>? emailMessages = _unitOfWork.EmailMessageRepository.GetAll().ToList();

            if (emailMessages == null) return NotFound();

            emailMessages.Reverse();

            var viewModel = new AdminEmailsViewModel
            {
                EmailMessages = emailMessages,
                UnreadEmailMessages = _unitOfWork.EmailMessageRepository.GetUnreadAmount()
            };

            return View(viewModel);
        }
    }
}
