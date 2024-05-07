using Microsoft.EntityFrameworkCore;
using portfolio.Models;

namespace portfolio.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Skill> Skills { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = 1, Name="C#"},
                new Skill { Id = 2, Name = "C#2"},
                new Skill { Id = 3, Name = "C#3"},
                new Skill { Id = 4, Name = "C#4"},
                new Skill { Id = 5, Name = "C#5"}
                );

            string description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida felis in ultrices molestie.";

            modelBuilder.Entity<Project>().HasData(
                new Project { Id = 1, Name="Project#1", Description = description, GitRepositoryUrl="git"},
                new Project { Id = 2, Name = "Project#2", Description = description, GitRepositoryUrl = "git" },
                new Project { Id = 3, Name = "Project#3", Description = description, GitRepositoryUrl = "git" }
                );

            modelBuilder.Entity<Contact>().HasData(
                new Contact { Id = 1, Name = "Facebook", Content = "/name.surname", UrlAddress = "https://www.facebook.com/", Icon = "bi bi-facebook" },
                new Contact { Id = 2, Name = "Sample 2", Content = "email_2@sample.com"},
                new Contact { Id = 3, Name = "Sample 3", Content = "email_3@sample.com"}
                );
        }

    }
}
