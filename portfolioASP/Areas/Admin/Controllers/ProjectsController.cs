using Microsoft.AspNetCore.Mvc;
using portfolio.DataAccess.Data;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Localization;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorization]
    public class ProjectsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHtmlLocalizer<ProjectsController> _localizer;

        public ProjectsController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IHtmlLocalizer<ProjectsController> localizer)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            List<Project> objProjectsList = _unitOfWork.ProjectRepository.GetAll().ToList();
            return View(objProjectsList);
        }

        public IActionResult Details(int? id)
        {
            if(id != null || id != 0)
            {
                Project? projectFromDb = _unitOfWork.ProjectRepository.Get(u => u.Id == id);

                if(projectFromDb == null)
                {
                    return NotFound();
                }

                return View(projectFromDb);
            }
            else
            {
                return NotFound();
            }

        }

        public IActionResult Upsert(int? id)
        {

            if(id == null || id == 0)
            {
                return View(new Project());
            }
            else
            {
                Project? projectFromDb = _unitOfWork.ProjectRepository.Get(u => u.Id == id);

                if (projectFromDb == null)
                {
                    return NotFound();
                }

                return View(projectFromDb);
            }
            
        }

        [HttpPost]
        public IActionResult Upsert(Project project, IFormFile? file)
        {
            if(ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\projects");

                    if(string.IsNullOrEmpty(project.ImageUrl) == false)
                    {
                        var oldImagePath = 
                            Path.Combine(wwwRootPath,project.ImageUrl.TrimStart('\\'));

                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    project.ImageUrl = @"\images\projects\" + fileName;
                }

                if(project.Id == 0)
                {
                    _unitOfWork.ProjectRepository.Add(project);
                    TempData["success"] = _localizer["ProjectWasAdded"].Value;
                }
                else
                {
                    TempData["success"] = _localizer["ProjectWasEdited"].Value;
                    _unitOfWork.ProjectRepository.Update(project);
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
                
            }

            return View(project);
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = _localizer["IdNotProvided"].Value });
            }

            Project? project = _unitOfWork.ProjectRepository.Get(u => u.Id == id);

            if (project == null)
            {
                return Json(new { success = false, message = _localizer["IdNotFound"].Value });
            }

            if (project.ImageUrl != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                var oldImagePath =
                    Path.Combine(wwwRootPath, project.ImageUrl.TrimStart('\\'));


                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _unitOfWork.ProjectRepository.Remove(project);
            _unitOfWork.Save();
            return Json(new { success = true, message = _localizer["ProjectWasRemoved"].Value });
        }
    }
}
