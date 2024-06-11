using portfolio.DataAccess.Data;
using portfolio.Models.ConfigureData;
using portfolio.Models.Email;
using System.Net;
using System.Net.Mail;

namespace portfolio.Utility.Email
{
    public class EmailService : IEmailService
    {
        private readonly ApplicationDbContext _dbContext;

        public EmailService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task SendEmailAsync(string email, string? subject, string? content)
        {
            ConfigureData? configureData = _dbContext.ConfigureDatas.Find(2);
            EmailSettings? emailSettingsDB = null;

            if (configureData != null)
            {
                emailSettingsDB = configureData.Convert<EmailSettings>();
            }

            if (emailSettingsDB != null)
            {
                var client = new SmtpClient(emailSettingsDB.SmtpServer, emailSettingsDB.SmtpPort)
                {
                    EnableSsl = emailSettingsDB.Encryption,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailSettingsDB.Email, emailSettingsDB.Password)
                };

                return client.SendMailAsync(
                    new MailMessage(from: emailSettingsDB.Email,
                                    to: email,
                                    subject,
                                    content));
            }

            throw new Exception("EmailSettins error");

        }

    }
}
