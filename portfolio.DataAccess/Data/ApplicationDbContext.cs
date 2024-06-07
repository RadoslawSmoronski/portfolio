using Microsoft.EntityFrameworkCore;
using portfolio.Models;
using portfolio.Models.ConfigureData;
using portfolio.Models.Email;
using portfolio.Models.Project;
using portfolio.Models.Skill;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public DbSet<EmailMessage> EmailMessages { get; set; }
        public DbSet<ConfigureData> ConfigureDatas { get; set; }

        public int GetUnreadEmailMessagesAmount()
        {
            int i = 0;

            foreach (EmailMessage message in EmailMessages)
            {
                if (message.IsReaded == false)
                {
                    i++;
                }
            }

            return i;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = 1, NameENG="C#", NamePL = "PL C#" },
                new Skill { Id = 2, NameENG = "C#2", NamePL = "PL C#2" },
                new Skill { Id = 3, NameENG = "C#3", NamePL = "PL C#3" },
                new Skill { Id = 4, NameENG = "C#4", NamePL = "PL C#4" },
                new Skill { Id = 5, NameENG = "C#5", NamePL = "PL C#5" }
                );

            string description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida felis in ultrices molestie.";

            modelBuilder.Entity<Project>().HasData(
                new Project { Id = 1, NameENG ="Project#1", NamePL = "Projekt#1", DescriptionENG = description, DescriptionPL = "PL " + description, GitRepositoryUrl ="git"},
                new Project { Id = 2, NameENG = "Project#2", NamePL = "Projekt#2", DescriptionENG = description, DescriptionPL = "PL " + description, ProjectWebsiteUrl="git" },
                new Project { Id = 3, NameENG = "Project#3", NamePL = "Projekt#3", DescriptionENG = description, DescriptionPL = "PL " + description, GitRepositoryUrl = "git", ProjectWebsiteUrl="git" }
                );

            modelBuilder.Entity<Contact>().HasData(
                new Contact { Id = 1, Name = "Facebook", Content = "/name.surname", UrlAddress = "https://www.facebook.com/", Icon = "bi bi-facebook" },
                new Contact { Id = 2, Name = "Sample 2", Content = "email_2@sample.com"},
                new Contact { Id = 3, Name = "Sample 3", Content = "email_3@sample.com"}
                );

            modelBuilder.Entity<EmailMessage>().HasData(
                new EmailMessage { Id = 1, Email = "test@email.com", Name = "John", Subject = "Test Subject", Content = description},
                new EmailMessage { Id = 2, Email = "test2@email.com", Name = "Mark", Subject = "Test Subject 2", Content = description },
                new EmailMessage { Id = 3, Email = "test3@email.com", Name = "Jeniffer", Subject = "Test Subject 3", Content = description }
                );

            AdminLogin adminLogin = new AdminLogin
            {
                Password = "$2a$11$8WGPCFiXVzavlpu6KaqakO738nLjnUrvioepPN0VwnQ3SD6SZZKUS"
            };

            EmailSettings emailSettings = new EmailSettings
            {
                Email = "portfolio.asp.test.email@gmail.com",
                Password = "lexmlhvkvyjognkf",
                SmtpServer = "smtp.gmail.com",
                SmtpPort = 587,
                Encryption = true
            };

            modelBuilder.Entity<ConfigureData>().HasData(
                new ConfigureData { Id = 1, JSON = adminLogin.GetJson()},
                new ConfigureData { Id = 2, JSON = emailSettings.GetJson()}
                );
        }

    }
}
