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

        public WebsiteTitleView(WebsiteTitle websiteTitle, string languageCode)
        {
            ImageUrl = websiteTitle.ImageUrl;

            if (languageCode == "pl")
            {
                Title = websiteTitle.TitlePL;
            }
            else
            {
                Title = websiteTitle.TitleENG;
            }
        }
    }
}
