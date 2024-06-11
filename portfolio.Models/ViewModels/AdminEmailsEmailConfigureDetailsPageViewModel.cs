using portfolio.Models.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.Models.ViewModels
{
    public class AdminEmailsEmailConfigureDetailsPageViewModel
    {
        public AutoEmailMessageContent? EmailMessageContent { get; set; }
        public EmailSettings? EmailSettings { get; set; }
    }
}
