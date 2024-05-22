using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.Models.ViewModels
{
    public class AdminGeneralViewModel
    {
        public WebsiteTitle.WebsiteTitle WebsiteTitle { get; set; }
        public NavbarLogo NavbarLogo { get; set; }
        public Welcome Welcome { get; set; }
        public Footer Footer { get; set; }

        public EditAdminLogin EditAdminLogin { get; set; }
    }
}
