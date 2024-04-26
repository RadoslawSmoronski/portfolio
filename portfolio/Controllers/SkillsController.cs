using Microsoft.AspNetCore.Mvc;
using portfolio.DataAccess.Data;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;

namespace portfolioASP.Controllers
{
    public class SkillsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SkillsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Skill> objSkillsList = _unitOfWork.SkillRepository.GetAll().ToList();
            return View(objSkillsList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Skill obj)
        {
            if(ModelState.IsValid)
            {
                obj.ImageUrl = "images/csharp.png";
                _unitOfWork.SkillRepository.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Umiejętność zostałą dodana!";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Skill? skillFromDb = _unitOfWork.SkillRepository.Get(u => u.Id == id);

            if(skillFromDb == null)
            {
                return NotFound();
            }

            return View(skillFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Skill obj)
        {
            if (ModelState.IsValid)
            {
                obj.ImageUrl = "images/csharp.png";
                _unitOfWork.SkillRepository.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Umiejętność została edytowana!";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Skill? skillFromDb = _unitOfWork.SkillRepository.Get(u => u.Id == id);

            if (skillFromDb == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.SkillRepository.Remove(skillFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Umiejętność została usunięta.";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
