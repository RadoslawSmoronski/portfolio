using portfolio.Models.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace portfolio.Models.Footer
{
    public class FooterView
    {
        public string? Content { get; set; }

        public FooterView(Footer footer, string langCode)
        {
            Content = langCode == "pl" ? footer.ContentPL : footer.ContentENG;
        }
    }
}
