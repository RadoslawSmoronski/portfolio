using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using portfolio.DataAccess.Json;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;
using portfolio.Models.Email;
using portfolio.Models.ViewModels;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorization]
    public class ContactController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EmailSettings _emailSettings;

        public ContactController(IUnitOfWork unitOfWork, IOptions<EmailSettings> emailSettings)
        {
            _unitOfWork = unitOfWork;
            _emailSettings = emailSettings.Value;
        }

        public IActionResult Index()
        {
            List<EmailMessage>? emailMessages = _unitOfWork.EmailMessageRepository.GetAll().ToList();

            if (emailMessages == null) return NotFound();

            emailMessages.Reverse();

            var viewModel = new AdminEmailsViewModel
            {
                EmailMessages = emailMessages,
                UnreadEmailMessages = _unitOfWork.EmailMessageRepository.GetUnreadAmount()
            };

            return View(viewModel);
        }

        public IActionResult Details(int? id)
        {
            EmailMessage? emailMessage = _unitOfWork.EmailMessageRepository.Get(u => u.Id == id);

            if (emailMessage == null) return NotFound();

            emailMessage.IsReaded = true;

            _unitOfWork.EmailMessageRepository.Update(emailMessage);
            _unitOfWork.Save();

            return View(emailMessage);
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "Nie przyjeto id" });
            }

            EmailMessage? emailMessage = _unitOfWork.EmailMessageRepository.Get(u => u.Id == id);

            if (emailMessage == null)
            {
                return Json(new { success = false, message = "Nie znaleziono podanego id" });
            }

            _unitOfWork.EmailMessageRepository.Remove(emailMessage);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Usunieto wiadomość" });
        }

        //EmailConfigure

        public IActionResult EmailConfigure()
        {
            var viewModel = new AdminEmailsEmailConfigureDetailsPageViewModel
            {
                EmailMessageContent = JsonFileManager<AutoEmailMessageContent>.Get(),
                EmailSettings = _emailSettings
            };

            viewModel.EmailSettings.Password = null;

            return View("EmailConfigure/Details", viewModel);
        }

        public IActionResult EmailEdit()
        {
            EmailSettings emailSettings = _emailSettings;
            emailSettings.Password = null;

            return View("EmailConfigure/EmailEdit", emailSettings);
        }

        [HttpPost]
        public IActionResult EmailEdit(EmailSettings emailSettings)
        {

            try
            {
                emailSettings.CheckConnection();

                TempData["success"] = "Dane zostały zmienione pomyślnie.";
                return RedirectToAction("EmailConfigure");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;

                EmailSettings emailSettingsObj = _emailSettings;
                emailSettingsObj.Password = null;

                return View("EmailConfigure/EmailEdit", emailSettingsObj);
            }
        }

        public IActionResult MessageEdit()
        {
            AutoEmailMessageContent? message = JsonFileManager<AutoEmailMessageContent>.Get();

            if (message == null) return NotFound();

            return View("EmailConfigure/MessageEdit", message);
        }

        [HttpPost]
        public IActionResult MessageEdit(AutoEmailMessageContent message)
        {

            try
            {
                JsonFileManager<AutoEmailMessageContent>.Save(message);

                TempData["success"] = "Wiadomość zostałą zmieniona.";
                return RedirectToAction("EmailConfigure");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;

                return View(message);
            }
        }

        //Contacts

        public IActionResult ContactsIndex()
        {
            List<Contact> objContactsList = _unitOfWork.ContactRepository.GetAll().ToList();
            return View("Contacts/ContactsIndex", objContactsList);
        }

        public IActionResult ContactsDetails(int? id)
        {
            if (id != null || id != 0)
            {
                Contact? contactFromDb = _unitOfWork.ContactRepository.Get(u => u.Id == id);

                if (contactFromDb == null)
                {
                    return NotFound();
                }

                return View("Contacts/ContactsDetails", contactFromDb);
            }
            else
            {
                return NotFound();
            }

        }

        public IActionResult ContactsUpsert(int? id)
        {

            if (id == null || id == 0)
            {
                return View("Contacts/ContactsUpsert", new Contact());
            }
            else
            {
                Contact? contactFromDb = _unitOfWork.ContactRepository.Get(u => u.Id == id);

                if (contactFromDb == null)
                {
                    return NotFound();
                }

                return View("Contacts/ContactsUpsert", contactFromDb);
            }

        }

        [HttpPost]
        public IActionResult ContactsUpsert(Contact contact)
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
                return RedirectToAction("ContactsIndex");
            }
            else
            {
                return View(contact);
            }
        }

        [HttpDelete]
        public IActionResult ContactsDelete(int? id)
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
