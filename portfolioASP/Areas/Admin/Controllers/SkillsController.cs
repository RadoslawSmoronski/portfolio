using Microsoft.AspNetCore.Mvc;
using portfolio.DataAccess.Data;
using portfolio.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Localization;
using portfolio.Models.Skill;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorization]
    public class SkillsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHtmlLocalizer<SkillsController> _localizer;

        public SkillsController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IHtmlLocalizer<SkillsController> localizer)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            List<Skill> objSkillsList = _unitOfWork.SkillRepository.GetAll().ToList();
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
                Skill? skillFromDb = _unitOfWork.SkillRepository.Get(u => u.Id == id);

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
                    string productPath = Path.Combine(wwwRootPath, @"images\skills");

                    if(string.IsNullOrEmpty(skill.ImageUrl) == false)
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

                    skill.ImageUrl = @"\images\skills\" + fileName;
                }

                if(skill.Id == 0)
                {
                    _unitOfWork.SkillRepository.Add(skill);
                    TempData["success"] = _localizer["SkillWasAdded"].Value;
                }
                else
                {
                    _unitOfWork.SkillRepository.Update(skill);
                    TempData["success"] = _localizer["SkillWasEdited"].Value;
                }

                _unitOfWork.Save();
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

            Skill? skill = _unitOfWork.SkillRepository.Get(u => u.Id == id);

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

            _unitOfWork.SkillRepository.Remove(skill);
            _unitOfWork.Save();
            return Json(new { success = true, message = _localizer["SkillWasRemoved"].Value });
        }
    }
}
