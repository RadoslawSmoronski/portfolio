using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using portfolio.Models.AboutMe;
using portfolio.Models.Skill;
using portfolio.Models.Project;
using portfolio.Models.Welcome;

namespace portfolio.Models.ViewModels
{
    public class ViewHomePageViewModel
    {
        public WelcomeView WelcomeView { get; set; }
        public List<SkillView> SkillViews { get; set; }
        public List<ProjectView> ProjectViews { get; set; }
        public List<Contact> Contacts { get; set; }
        public AboutMeView AboutMeView { get; set; }

    }
}
