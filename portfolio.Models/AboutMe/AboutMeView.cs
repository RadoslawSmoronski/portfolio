using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace portfolio.Models.AboutMe
{
    public class AboutMeView
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public AboutMeView(AboutMe aboutMe, string langCode)
        {
            ImageUrl = aboutMe.ImageUrl;

            if (langCode == "pl")
            {
                Description = aboutMe.DescriptionPL;
                Title = aboutMe.TitlePL;
            }
            else
            {
                Description = aboutMe.DescriptionENG;
                Title = aboutMe.TitleENG;
            }
        }
    }
}
