namespace portfolio.Models.WebsiteTab
{
    public class WebsiteTabView
    {
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }

        public WebsiteTabView(WebsiteTab websiteTab, string langCode)
        {
            ImageUrl = websiteTab.ImageUrl;
            Title = langCode == "pl" ? websiteTab.TitlePL : websiteTab.TitleENG;
        }
    }
}
