namespace portfolio.Models.Welcome
{
    public class WelcomeView
    {
        public string Header { get; set; }
        public string Description { get; set; }

        public WelcomeView(Welcome welcome, string langCode)
        {
            if(langCode == "pl")
            {
                Header = welcome.HeaderPL;
                Description = welcome.DescriptionPL;
            }
            else
            {
                Header = welcome.HeaderENG;
                Description = welcome.DescriptionENG;
            }
        }
    }
}
