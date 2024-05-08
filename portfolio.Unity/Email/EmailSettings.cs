namespace portfolio.Utility.Email
{
    public class EmailSettings
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set;}
        public bool EnableSsl { get; set; }

    }
}
