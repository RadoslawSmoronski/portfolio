using portfolio.Models.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace portfolio.Models.Navbar
{
    public class NavbarView
    {
        public string? Header { get; set; }
        public string? ImageUrl { get; set; } = null;

        public NavbarView(Navbar navbar, string langCode)
        {
            ImageUrl = navbar.ImageUrl;
            Header = langCode == "pl" ? navbar.HeaderPL : navbar.HeaderENG;
        }
    }
}
