namespace portfolio.Models.Email
{
    public class AutoEmailMessageContentView
    {
        public string? Subject { get; set; }
        public string? Content { get; set; }

        public AutoEmailMessageContentView(AutoEmailMessageContent autoEmailMessageContent, string langCode)
        {
            if (langCode == "pl")
            {
                Subject = autoEmailMessageContent.SubjectPL;
                Content = autoEmailMessageContent.ContentPL;
            }
            else
            {
                Subject = autoEmailMessageContent.SubjectENG;
                Content = autoEmailMessageContent.ContentENG;
            }
        }
    }
}
