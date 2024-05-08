using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using portfolio.DataAccess.Json;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;
using portfolio.Utility.Email;
using System.Drawing.Drawing2D;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactFormController : Controller
    {
        private readonly EmailSettings _emailSettings;

        public ContactFormController(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public IActionResult Index()
        {
            return View(_emailSettings);
        }

        //public IActionResult Edit()
        //{
        //    string description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean vel augue purus. Etiam imperdiet dui a dui ultricies, eget sagittis lacus porttitor.Etiam id eleifend sapien. Suspendisse tempus mauris maximus fringilla rhoncus. Mauris vel nisi mollis, varius ex in, maximus enim. Aenean iaculis lobortis sem sed hendrerit.";

        //    AboutMe aboutMe = new AboutMe { Title = "Name and Surname", Description = description };

        //    return View(_jsonFileManager.AboutMe);
        //}

        //[HttpPost]
        //public IActionResult Edit(AboutMe aboutMe, IFormFile? file)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string wwwRootPath = _webHostEnvironment.WebRootPath;
        //        if (file != null)
        //        {
        //            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        //            string productPath = Path.Combine(wwwRootPath, @"images\aboutme");

        //            if (string.IsNullOrEmpty(aboutMe.ImageUrl) == false)
        //            {
        //                var oldImagePath =
        //                    Path.Combine(wwwRootPath, aboutMe.ImageUrl.TrimStart('\\'));

        //                if (System.IO.File.Exists(oldImagePath))
        //                {
        //                    System.IO.File.Delete(oldImagePath);
        //                }
        //            }

        //            using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
        //            {
        //                file.CopyTo(fileStream);
        //            }

        //            aboutMe.ImageUrl = @"\images\aboutme\" + fileName;
        //        }

        //        _jsonFileManager.Edit(aboutMe);

        //        TempData["success"] = "Edytowałes O Mnie";
        //        return RedirectToAction("Index");

        //    }

        //    return View(aboutMe);
        //}

    }
}
