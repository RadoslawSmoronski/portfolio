using Microsoft.AspNetCore.Mvc;
using portfolio.DataAccess.Repository.IRepository;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            int unreadEmailMessages = _unitOfWork.EmailMessageRepository.GetUnreadAmount();

            return View(unreadEmailMessages);
        }

    }
}
