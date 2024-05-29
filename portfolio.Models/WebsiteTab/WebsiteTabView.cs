using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
