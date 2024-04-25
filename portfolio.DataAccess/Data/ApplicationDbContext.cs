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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = 1, Name="C#", ImageUrl="images/csharp.png"},
                new Skill { Id = 2, Name = "C#2", ImageUrl = "images/csharp.png" },
                new Skill { Id = 3, Name = "C#3", ImageUrl = "images/csharp.png" },
                new Skill { Id = 4, Name = "C#4", ImageUrl = "images/csharp.png" },
                new Skill { Id = 5, Name = "C#5", ImageUrl = "images/csharp.png" }
                );
        }

    }
}
