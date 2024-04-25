using Microsoft.AspNetCore.Mvc;
using portfolio.Data;
using portfolio.Models;

namespace portfolio.Controllers
{
    public class SkillsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SkillsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Skill> objSkillsList = _db.Skills.ToList();
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
                _db.Skills.Add(obj);
                _db.SaveChanges();
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

            Skill? skillFromDb = _db.Skills.Find(id);

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
                _db.Skills.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost, ActionName("delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Skill? skillFromDb = _db.Skills.Find(id);

            if (skillFromDb == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Skills.Remove(skillFromDb);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
