using Microsoft.AspNetCore.Mvc;
using portfolio.DataAccess.Data;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Contact> objContactsList = _unitOfWork.ContactRepository.GetAll().ToList();
            return View(objContactsList);
        }

        public IActionResult Details(int? id)
        {
            if (id != null || id != 0)
            {
                Contact? contactFromDb = _unitOfWork.ContactRepository.Get(u => u.Id == id);

                if (contactFromDb == null)
                {
                    return NotFound();
                }

                return View(contactFromDb);
            }
            else
            {
                return NotFound();
            }

        }

        public IActionResult Upsert(int? id)
        {

            if (id == null || id == 0)
            {
                return View(new Contact());
            }
            else
            {
                Contact? contactFromDb = _unitOfWork.ContactRepository.Get(u => u.Id == id);

                if (contactFromDb == null)
                {
                    return NotFound();
                }

                return View(contactFromDb);
            }

        }

        [HttpPost]
        public IActionResult Upsert(Contact contact)
        {
            if (ModelState.IsValid)
            {

                if (contact.Id == 0)
                {
                    _unitOfWork.ContactRepository.Add(contact);
                }
                else
                {
                    _unitOfWork.ContactRepository.Update(contact);
                }

                _unitOfWork.Save();
                TempData["success"] = "Kontakt został utworzony.";
                return RedirectToAction("Index");

            }
            else
            {
                return View(contact);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "Nie przyjeto id" });
            }

            Contact? contactFromDb = _unitOfWork.ContactRepository.Get(u => u.Id == id);

            if (contactFromDb == null)
            {
                return Json(new { success = false, message = "Nie znaleziono podanego id" });
            }

            _unitOfWork.ContactRepository.Remove(contactFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Usunieto kontakt" });
        }
    }
}
