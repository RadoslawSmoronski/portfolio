using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace portfolio.Models.NavbarLogo
{
    public class NavbarLogoView
    {
        public string? Title { get; set; }
        public string? ImageUrl { get; set; } = null;

        public NavbarLogoView(NavbarLogo navbarLogo, string languageCode)
        {
            ImageUrl = navbarLogo.ImageUrl;

            if (languageCode == "pl")
            {
                Title = navbarLogo.TitlePL;
            }
            else
            {
                Title = navbarLogo.TitleENG;
            }
        }
    }
}
