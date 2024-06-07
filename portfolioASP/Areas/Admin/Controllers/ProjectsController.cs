using Microsoft.AspNetCore.Mvc;
using portfolio.DataAccess.Data;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Localization;
using portfolio.Models.Project;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorization]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHtmlLocalizer<ProjectsController> _localizer;

        public ProjectsController(
            ApplicationDbContext dbcontext,
            IWebHostEnvironment webHostEnvironment,
            IHtmlLocalizer<ProjectsController> localizer)
        {
            _dbcontext = dbcontext;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            List<Project> objProjectsList = _dbcontext.Projects.ToList();
            return View(objProjectsList);
        }

        public IActionResult Details(int? id)
        {
            if(id != null || id != 0)
            {
                Project? projectFromDb = _dbcontext.Projects.Find(id);

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
                Project? projectFromDb = _dbcontext.Projects.Find(id);

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
                    string productPath = Path.Combine(wwwRootPath, "images", "projects");

                    if (string.IsNullOrEmpty(project.ImageUrl) == false)
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

                    project.ImageUrl = Path.Combine("images", "projects", fileName);
                }

                if(project.Id == 0)
                {
                    _dbcontext.Projects.Add(project);
                    TempData["success"] = _localizer["ProjectWasAdded"].Value;
                }
                else
                {
                    _dbcontext.Projects.Update(project);
                    TempData["success"] = _localizer["ProjectWasEdited"].Value;
                }

                _dbcontext.SaveChanges();
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

            Project? project = _dbcontext.Projects.Find(id);

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

            _dbcontext.Projects.Remove(project);
            _dbcontext.SaveChanges();
            return Json(new { success = true, message = _localizer["ProjectWasRemoved"].Value });
        }
    }
}
