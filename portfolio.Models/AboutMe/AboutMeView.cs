namespace portfolio.Models.AboutMe
{
    public class AboutMeView
    {
        public string? Header { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }

        public AboutMeView(AboutMe aboutMe, string langCode)
        {
            ImageUrl = aboutMe.ImageUrl;

            if (langCode == "pl")
            {
                Content = aboutMe.ContentPL;
                Header = aboutMe.HeaderPL;
            }
            else
            {
                Content = aboutMe.ContentENG;
                Header = aboutMe.HeaderENG;
            }
        }
    }
}
