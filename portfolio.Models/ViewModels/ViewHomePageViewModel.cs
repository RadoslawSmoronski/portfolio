using portfolio.Models.AboutMe;
using portfolio.Models.Project;
using portfolio.Models.Skill;
using portfolio.Models.Welcome;

namespace portfolio.Models.ViewModels
{
    public class ViewHomePageViewModel
    {
        public WelcomeView? WelcomeView { get; set; }
        public List<SkillView>? SkillViews { get; set; }
        public List<ProjectView>? ProjectViews { get; set; }
        public List<Contact>? Contacts { get; set; }
        public AboutMeView? AboutMeView { get; set; }

    }
}
