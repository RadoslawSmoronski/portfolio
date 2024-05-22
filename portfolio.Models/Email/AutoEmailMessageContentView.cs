using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace portfolio.Models.Email
{
    public class AutoEmailMessageContentView
    {
        public string? Subject { get; set; }
        public string? Content { get; set; }

        public AutoEmailMessageContentView(AutoEmailMessageContent autoEmailMessageContent, string languageCode)
        {
            if (languageCode == "pl")
            {
                Subject = autoEmailMessageContent.SubjectPL;
                Content = autoEmailMessageContent.ContentPL;
            }
            else
            {
                Subject = autoEmailMessageContent.SubjectENG;
                Content = autoEmailMessageContent.ContentENG;
            }
        }
    }
}
