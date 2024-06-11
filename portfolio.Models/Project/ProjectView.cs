namespace portfolio.Models.Project
{
    public class ProjectView
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string? GitRepositoryUrl { get; set; }
        public string? ProjectWebsiteUrl { get; set; }
        public string? ImageUrl { get; set; }

        public ProjectView(Project project, string langCode)
        {
            ImageUrl = project.ImageUrl;
            GitRepositoryUrl = project.GitRepositoryUrl;
            ProjectWebsiteUrl = project.ProjectWebsiteUrl;

            if (langCode == "pl")
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
