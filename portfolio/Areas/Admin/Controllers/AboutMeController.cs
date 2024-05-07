using Azure;
using Microsoft.AspNetCore.Mvc;
using portfolio.DataAccess.Data;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;
using System.Drawing.Drawing2D;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutMeController : Controller
    {

        public AboutMeController()
        {
        }
        public IActionResult Index()
        {
            string description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean vel augue purus. Etiam imperdiet dui a dui ultricies, eget sagittis lacus porttitor.Etiam id eleifend sapien. Suspendisse tempus mauris maximus fringilla rhoncus. Mauris vel nisi mollis, varius ex in, maximus enim. Aenean iaculis lobortis sem sed hendrerit.";

            AboutMe aboutMe = new AboutMe { Title = "Name and Surname", Description = description };

            return View(aboutMe);
        }

        public IActionResult Edit()
        {
            string description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean vel augue purus. Etiam imperdiet dui a dui ultricies, eget sagittis lacus porttitor.Etiam id eleifend sapien. Suspendisse tempus mauris maximus fringilla rhoncus. Mauris vel nisi mollis, varius ex in, maximus enim. Aenean iaculis lobortis sem sed hendrerit.";

            AboutMe aboutMe = new AboutMe { Title = "Name and Surname", Description = description };

            return View(aboutMe);
        }

        //[HttpPost]
        //public IActionResult Upsert(Skill skill, IFormFile? file)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        string wwwRootPath = _webHostEnvironment.WebRootPath;
        //        if(file != null)
        //        {
        //            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        //            string productPath = Path.Combine(wwwRootPath, @"images\skills");

        //            if(string.IsNullOrEmpty(skill.ImageUrl) == false)
        //            {
        //                var oldImagePath = 
        //                    Path.Combine(wwwRootPath,skill.ImageUrl.TrimStart('\\'));

        //                if(System.IO.File.Exists(oldImagePath))
        //                {
        //                    System.IO.File.Delete(oldImagePath);
        //                }
        //            }

        //            using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
        //            {
        //                file.CopyTo(fileStream);
        //            }

        //            skill.ImageUrl = @"\images\skills\" + fileName;
        //        }

        //        if(skill.Id == 0)
        //        {
        //            _unitOfWork.SkillRepository.Add(skill);
        //        }
        //        else
        //        {
        //            _unitOfWork.SkillRepository.Update(skill);
        //        }

        //        _unitOfWork.Save();
        //        TempData["success"] = "Umiejętność zostałą utworzona";
        //        return RedirectToAction("Index");

        //    }

        //    return View(skill);
        //}

    }
}
