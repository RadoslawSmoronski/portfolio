using portfolio.Models.Email;

namespace portfolio.Models.ViewModels
{
    public class AdminEmailsViewModel
    {
        public List<EmailMessage>? EmailMessages { get; set; }
        public int UnreadEmailMessages { get; set; }
    }
}
