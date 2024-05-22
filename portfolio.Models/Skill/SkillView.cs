using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace portfolio.Models.Skill
{
    public class SkillView
    {
        public string Name { get; set; }
        public string? ImageUrl { get; set; }

        public SkillView(Skill skill, string languageCode)
        {
            ImageUrl = skill.ImageUrl;

            if (languageCode == "pl")
            {
                Name = skill.NamePL;
            }
            else
            {
                Name = skill.NameENG;
            }
        }
    }
}
