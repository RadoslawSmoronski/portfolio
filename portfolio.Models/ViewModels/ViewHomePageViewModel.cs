using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using portfolio.Models.AboutMe;

namespace portfolio.Models.ViewModels
{
    public class ViewHomePageViewModel
    {
        public Welcome Welcome { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Project> Projects { get; set; }

        public List<Contact> Contacts { get; set; }
        public AboutMeView AboutMeView { get; set; }

    }
}
