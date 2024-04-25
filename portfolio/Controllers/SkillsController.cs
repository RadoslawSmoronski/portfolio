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
    }
}
