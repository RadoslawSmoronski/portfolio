using portfolio.Models.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.Models.ViewModels
{
    public class AdminEmailsViewModel
    {
        public List<EmailMessage>? EmailMessages { get; set; }
        public int UnreadEmailMessages { get; set; }
    }
}
