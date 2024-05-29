using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace portfolio.Models.WebsiteTitle
{
    public class WebsiteTitleView
    {
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }

        public WebsiteTitleView(WebsiteTitle websiteTitle, string langCode)
        {
            ImageUrl = websiteTitle.ImageUrl;
            Title = langCode == "pl" ? websiteTitle.TitlePL : websiteTitle.TitleENG;
        }
    }
}
