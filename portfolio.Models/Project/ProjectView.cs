using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.Models.Project
{
    public class ProjectView
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string? GitRepositoryUrl { get; set; }
        public string? ProjectWebsiteUrl { get; set; }
        public string? ImageUrl { get; set; }

        public ProjectView(Project project, string languageCode)
        {
            ImageUrl = project.ImageUrl;
            GitRepositoryUrl = project.GitRepositoryUrl;
            ProjectWebsiteUrl = project.ProjectWebsiteUrl;

            if (languageCode == "pl")
            {
                Name = project.NamePL;
                Description = project.DescriptionPL;
            }
            else
            {
                Name = project.NameENG;
                Description = project.DescriptionENG;
            }
        }
    }
}
