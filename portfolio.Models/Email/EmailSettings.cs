using System.Net.Mail;
using System.Net;
using System.ComponentModel.DataAnnotations;
using portfolio.Models.ConfigureData;
using Newtonsoft.Json;

namespace portfolio.Models.Email
{
    public class EmailSettings : IConfigureDataClass
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? SmtpServer { get; set; }
        [Required]
        public int SmtpPort { get; set;}
        [Required]
        public bool Encryption  { get; set; }

        public string GetJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public bool CheckConnection()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Email))
                {
                    throw new ArgumentException("Email value is empty or null.");
                }

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
            catch (Exception)
            {
                return false;
            }
        }

    }
}
