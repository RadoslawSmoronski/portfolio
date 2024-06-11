using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using portfolio.DataAccess.Data;
using portfolio.Models.Skill;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorization]
    public class SkillsController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHtmlLocalizer<SkillsController> _localizer;

        public SkillsController(ApplicationDbContext dbContext,
            IWebHostEnvironment webHostEnvironment,
            IHtmlLocalizer<SkillsController> localizer)
        {
            _dbcontext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            List<Skill> objSkillsList = _dbcontext.Skills.ToList();
            return View(objSkillsList);
        }

        public IActionResult Upsert(int? id)
        {

            if(id == null || id == 0)
            {
                return View(new Skill());
            }
            else
            {
                Skill? skillFromDb = _dbcontext.Skills.Find(id);

                if (skillFromDb == null)
                {
                    return NotFound();
                }

                return View(skillFromDb);
            }
            
        }

        [HttpPost]
        public IActionResult Upsert(Skill skill, IFormFile? file)
        {
            if(ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, "images", "skills");

                    if (string.IsNullOrEmpty(skill.ImageUrl) == false)
                    {
                        var oldImagePath = 
                            Path.Combine(wwwRootPath,skill.ImageUrl.TrimStart('\\'));

                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    skill.ImageUrl = Path.Combine("images", "skills", fileName);
                }

                if(skill.Id == 0)
                {
                    _dbcontext.Skills.Add(skill);
                    TempData["success"] = _localizer["SkillWasAdded"].Value;
                }
                else
                {
                    _dbcontext.Skills.Update(skill);
                    TempData["success"] = _localizer["SkillWasEdited"].Value;
                }

                _dbcontext.SaveChanges();
                return RedirectToAction("Index");
                
            }

            return View(skill);
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = _localizer["IdNotProvided"].Value });
            }

            Skill? skill = _dbcontext.Skills.Find(id);

            if (skill == null)
            {
                return Json(new { success = false, message = _localizer["IdNotFound"].Value });
            }

            if(skill.ImageUrl != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                var oldImagePath =
                    Path.Combine(wwwRootPath, skill.ImageUrl.TrimStart('\\'));


                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _dbcontext.Skills.Remove(skill);
            _dbcontext.SaveChanges();
            return Json(new { success = true, message = _localizer["SkillWasRemoved"].Value });
        }
    }
}
