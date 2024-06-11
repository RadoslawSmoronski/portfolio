using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using portfolio.DataAccess.Data;
using portfolio.DataAccess.Json;
using portfolio.Models;
using portfolio.Models.ConfigureData;
using portfolio.Models.Email;
using portfolio.Models.ViewModels;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorization]
    public class ContactController : Controller
    {
        private readonly IHtmlLocalizer<ContactController> _localizer;
        private readonly IJsonFileManager _jsonFileManager;
        private readonly ApplicationDbContext _dbContext;

        public ContactController(
            IHtmlLocalizer<ContactController> localizer,
            IJsonFileManager jsonFileManager,
            ApplicationDbContext dbContext)
        {
            _localizer = localizer;
            _jsonFileManager = jsonFileManager;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<EmailMessage>? emailMessages = _dbContext.EmailMessages.ToList();

            if (emailMessages == null) return NotFound();

            emailMessages.Reverse();

            var viewModel = new AdminEmailsViewModel
            {
                EmailMessages = emailMessages,
                UnreadEmailMessages = _dbContext.GetUnreadEmailMessagesAmount()
            };

            return View(viewModel);
        }

        public IActionResult Details(int? id)
        {
            EmailMessage? emailMessage = _dbContext.EmailMessages.Find(id);

            if (emailMessage == null) return NotFound();

            emailMessage.IsReaded = true;

            _dbContext.EmailMessages.Update(emailMessage);
            _dbContext.SaveChanges();

            return View(emailMessage);
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = _localizer["IdNotProvided"].Value });
            }

            EmailMessage? emailMessage = _dbContext.EmailMessages.Find(id);

            if (emailMessage == null)
            {
                return Json(new { success = false, message = _localizer["IdNotFound"].Value });
            }

            _dbContext.EmailMessages.Remove(emailMessage);
            _dbContext.SaveChanges();
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
            List<Contact> objContactsList = _dbContext.Contacts.ToList();
            return View("Contacts/ContactsIndex", objContactsList);
        }

        public IActionResult ContactsDetails(int? id)
        {
            if (id != null || id != 0)
            {
                Contact? contactFromDb = _dbContext.Contacts.Find(id);

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
                Contact? contactFromDb = _dbContext.Contacts.Find(id);

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
                    _dbContext.Contacts.Add(contact);
                    TempData["success"] = _localizer["ContactHasBeenCreated"].Value;
                }
                else
                {
                    _dbContext.Contacts.Update(contact);
                    TempData["success"] = _localizer["ContactHasBeenEdited"].Value;
                }

                _dbContext.SaveChanges();
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

            Contact? contactFromDb = _dbContext.Contacts.Find(id);

            if (contactFromDb == null)
            {
                return Json(new { success = false, message = _localizer["IdNotFound"].Value });
            }

            _dbContext.Contacts.Remove(contactFromDb);
            _dbContext.SaveChanges();
            return Json(new { success = true, message = _localizer["MessageHasBeenDeleted"].Value });
        }
    }
}
