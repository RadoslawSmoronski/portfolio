using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using portfolio.DataAccess.Json;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models.Email;
using portfolio.Models.ViewModels;
using portfolio.Utility;
using Microsoft.AspNetCore.Mvc.Localization;
using portfolio.Models;
using portfolio.DataAccess.Data;
using portfolio.Models.ConfigureData;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorization]
    public class ContactController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHtmlLocalizer<ContactController> _localizer;
        private readonly IJsonFileManager _jsonFileManager;
        private readonly ApplicationDbContext _dbContext;

        public ContactController(IUnitOfWork unitOfWork,
            IHtmlLocalizer<ContactController> localizer,
            IJsonFileManager jsonFileManager,
            ApplicationDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
            _jsonFileManager = jsonFileManager;
            _dbContext = dbContext;
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
                return Json(new { success = false, message = _localizer["IdNotProvided"].Value });
            }

            EmailMessage? emailMessage = _unitOfWork.EmailMessageRepository.Get(u => u.Id == id);

            if (emailMessage == null)
            {
                return Json(new { success = false, message = _localizer["IdNotFound"].Value });
            }

            _unitOfWork.EmailMessageRepository.Remove(emailMessage);
            _unitOfWork.Save();
            return Json(new { success = true, message = _localizer["MessageHasBeenDeleted"].Value });
        }

        //EmailConfigure

        public IActionResult EmailConfigure()
        {
            ConfigureData? configureData = _dbContext.ConfigureDatas.Find(2);
            EmailSettings? emailSettingsDB = null;

            if (configureData != null)
            {
                emailSettingsDB = configureData.Convert<EmailSettings>();
            }

            if(emailSettingsDB != null)
            {
                var viewModel = new AdminEmailsEmailConfigureDetailsPageViewModel
                {
                    EmailMessageContent = _jsonFileManager.Get<AutoEmailMessageContent>(),
                    EmailSettings = emailSettingsDB
                };

                viewModel.EmailSettings.Password = null;

                return View("EmailConfigure/Details", viewModel);
            }

            TempData["error"] = _localizer["EmailSettingsError"].Value; ;
            return RedirectToAction("Index");
        }

        public IActionResult EmailEdit()
        {
            ConfigureData? configureData = _dbContext.ConfigureDatas.Find(2);
            EmailSettings? emailSettingsDB = null;

            if (configureData != null)
            {
                emailSettingsDB = configureData.Convert<EmailSettings>();
            }

            if (emailSettingsDB != null)
            {
                EmailSettings emailSettingsObj = emailSettingsDB;
                emailSettingsObj.Password = null;

                return View("EmailConfigure/EmailEdit", emailSettingsObj);
            }

            TempData["error"] = _localizer["EmailSettingsError"].Value; ;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EmailEdit(EmailSettings emailSettings)
        {
            ConfigureData? configureData = _dbContext.ConfigureDatas.Find(2);
            EmailSettings? emailSettingsDB = null;

            if (configureData != null)
            {
                emailSettingsDB = configureData.Convert<EmailSettings>();
            }

            if (emailSettingsDB != null && configureData != null)
            {
                try
                {
                    emailSettings.CheckConnection();

                    emailSettingsDB = emailSettings;
                    configureData.JSON = emailSettingsDB.GetJson();
                    _dbContext.SaveChanges();

                    TempData["success"] = _localizer["EmailSettingsHasBeenEdited"].Value;
                    return RedirectToAction("EmailConfigure");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;

                    EmailSettings emailSettingsObj = emailSettingsDB;
                    emailSettingsObj.Password = null;

                    return View("EmailConfigure/EmailEdit", emailSettingsObj);

                }
            }


            TempData["error"] = _localizer["EmailSettingsError"].Value; ;
            return RedirectToAction("Index");
        }

        public IActionResult MessageEdit()
        {
            AutoEmailMessageContent? message = _jsonFileManager.Get<AutoEmailMessageContent>();

            if (message == null) return NotFound();

            return View("EmailConfigure/MessageEdit", message);
        }

        [HttpPost]
        public IActionResult MessageEdit(AutoEmailMessageContent message)
        {

            try
            {
                _jsonFileManager.Save<AutoEmailMessageContent>(message);

                TempData["success"] = _localizer["AutoEmailMessageHasBeenEdited"].Value;
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
                    TempData["success"] = _localizer["ContactHasBeenCreated"].Value;
                }
                else
                {
                    _unitOfWork.ContactRepository.Update(contact);
                    TempData["success"] = _localizer["ContactHasBeenEdited"].Value;
                }

                _unitOfWork.Save();
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
                return Json(new { success = false, message = _localizer["IdNotProvided"].Value });
            }

            Contact? contactFromDb = _unitOfWork.ContactRepository.Get(u => u.Id == id);

            if (contactFromDb == null)
            {
                return Json(new { success = false, message = _localizer["IdNotFound"].Value });
            }

            _unitOfWork.ContactRepository.Remove(contactFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = _localizer["MessageHasBeenDeleted"].Value });
        }
    }
}
