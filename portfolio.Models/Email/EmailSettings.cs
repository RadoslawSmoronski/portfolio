using System.Net.Mail;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace portfolio.Models.Email
{
    public class EmailSettings
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string SmtpServer { get; set; }
        [Required]
        public int SmtpPort { get; set;}
        [Required]
        public bool Encryption  { get; set; }

        public bool CheckConnection()
        {
            try
            {
                var client = new SmtpClient(SmtpServer, SmtpPort)
                {
                    EnableSsl = Encryption,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Email, Password)
                };

                MailMessage message = new MailMessage(from: Email, to: Email, "Connection test", "the connection test was successful");

                client.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
