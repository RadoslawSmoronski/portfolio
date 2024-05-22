using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.Models.Footer
{
    public class FooterView
    {
        public string? Content { get; set; }

        public FooterView(Footer footer, string languageCode)
        {

            if (languageCode == "pl")
            {
                Content = footer.ContentPL;
            }
            else
            {
                Content = footer.ContentENG;
            }
        }
    }
}
