using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using portfolio.Models.AboutMe;
using portfolio.Models.Skill;

namespace portfolio.Models.ViewModels
{
    public class ViewHomePageViewModel
    {
        public Welcome Welcome { get; set; }
        public List<SkillView> SkillViews { get; set; }
        public List<Project> Projects { get; set; }

        public List<Contact> Contacts { get; set; }
        public AboutMeView AboutMeView { get; set; }

    }
}
