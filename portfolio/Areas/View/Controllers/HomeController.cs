using Microsoft.AspNetCore.Mvc;
using portfolio.DataAccess.Data;
using portfolio.DataAccess.Json;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;
using portfolio.Models.ViewModels;
using System.Diagnostics;

namespace portfolioASP.Areas.View.Controllers
{
    [Area("View")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JsonFileManager _jsonFileManager;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, JsonFileManager jsonFileManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _jsonFileManager = jsonFileManager;
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
