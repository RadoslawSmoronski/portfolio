using Microsoft.Extensions.Options;
using portfolio.Models.Email;
using portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using portfolio.DataAccess.Repository.IRepository;

namespace portfolio.Utility.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public Task SendEmailAsync(string email, string? subject, string? content)
        {

            var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
            {
                EnableSsl = _emailSettings.Encryption,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password)
            };

            return client.SendMailAsync(
                new MailMessage(from: _emailSettings.Email,
                                to: email,
                                subject,
                                content));

        }

    }
}
