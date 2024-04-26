using Microsoft.AspNetCore.Mvc;
using portfolio.DataAccess.Data;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;

namespace portfolioASP.Controllers
{
    public class SkillsController : Controller
    {
        private readonly ISkillRepository _skillRepo;

        public SkillsController(ISkillRepository db)
        {
            _skillRepo = db;
        }

        public IActionResult Index()
        {
            List<Skill> objSkillsList = _skillRepo.GetAll().ToList();
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
                _skillRepo.Add(obj);
                _skillRepo.Save();
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

            Skill? skillFromDb = _skillRepo.Get(u => u.Id == id);

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
                _skillRepo.Update(obj);
                _skillRepo.Save();
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

            Skill? skillFromDb = _skillRepo.Get(u => u.Id == id);

            if (skillFromDb == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _skillRepo.Remove(skillFromDb);
                _skillRepo.Save();
                TempData["success"] = "Umiejętność została usunięta.";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
