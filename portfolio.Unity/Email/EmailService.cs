using portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.Utility.Email
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string content)
        {
            var mail = "portfolio.asp.test.email@gmail.com";
            var pw = "lexmlhvkvyjognkf";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(mail, pw)
            };

            return client.SendMailAsync(
                new MailMessage(from: mail,
                                to: email,
                                subject,
                                content));

        }
    }
}
