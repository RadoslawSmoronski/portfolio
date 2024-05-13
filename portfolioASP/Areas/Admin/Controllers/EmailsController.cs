﻿using Azure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using portfolio.DataAccess.Json;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;
using portfolio.Models.Email;
using portfolio.Models.ViewModels;
using portfolio.Utility.Email;
using System.Drawing.Drawing2D;

namespace portfolioASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmailsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EmailSettings _emailSettings;


        public EmailsController(IUnitOfWork unitOfWork, IOptions<EmailSettings> emailSettings)
        {
            _unitOfWork = unitOfWork;
            _emailSettings = emailSettings.Value;
        }

        public IActionResult Index()
        {
            List<EmailMessage>? emailMessages = _unitOfWork.EmailMessageRepository.GetAll().ToList();

            if (emailMessages == null) return NotFound();

            emailMessages.Reverse();

            AdminEmailsViewModel viewModel = new AdminEmailsViewModel();
            viewModel.EmailMessages = emailMessages;
            viewModel.UnreadEmailMessages = _unitOfWork.EmailMessageRepository.GetUnreadAmount();

            return View(viewModel);
        }

        public IActionResult Details(int? id)
        {
            EmailMessage? emailMessage = _unitOfWork.EmailMessageRepository.Get( u => u.Id == id );

            if (emailMessage == null) return NotFound();

            emailMessage.IsReaded = true;

            _unitOfWork.EmailMessageRepository.Update(emailMessage);
            _unitOfWork.Save();

            return View(emailMessage);
        }

        public IActionResult EmailConfigure()
        {
            AdminEmailsEmailConfigureDetailsPageViewModel viewModel = new AdminEmailsEmailConfigureDetailsPageViewModel();
            viewModel.EmailMessageContent = JsonFileManager<AutoEmailMessageContent>.Get();
            viewModel.EmailSettings = _emailSettings;

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

    }
}
