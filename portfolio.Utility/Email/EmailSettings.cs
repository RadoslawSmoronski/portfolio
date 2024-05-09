using System.Net.Mail;
using System.Net;

namespace portfolio.Utility.Email
{
    public class EmailSettings
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set;}
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
                throw;
            }
        }

    }
}
