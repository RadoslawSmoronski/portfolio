using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace portfolio.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddAllColumnsAndSeedThey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsReaded = table.Column<bool>(type: "bit", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameENG = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    NamePL = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    DescriptionENG = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DescriptionPL = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    GitRepositoryUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectWebsiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameENG = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    NamePL = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Content", "Icon", "Name", "UrlAddress" },
                values: new object[,]
                {
                    { 1, "/name.surname", "bi bi-facebook", "Facebook", "https://www.facebook.com/" },
                    { 2, "email_2@sample.com", null, "Sample 2", null },
                    { 3, "email_3@sample.com", null, "Sample 3", null }
                });

            migrationBuilder.InsertData(
                table: "EmailMessages",
                columns: new[] { "Id", "Content", "Email", "IsReaded", "Name", "SentAt", "Subject" },
                values: new object[,]
                {
                    { 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida felis in ultrices molestie.", "test@email.com", false, "John", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test Subject" },
                    { 2, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida felis in ultrices molestie.", "test2@email.com", false, "Mark", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test Subject 2" },
                    { 3, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida felis in ultrices molestie.", "test3@email.com", false, "Jeniffer", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test Subject 3" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "DescriptionENG", "DescriptionPL", "GitRepositoryUrl", "ImageUrl", "NameENG", "NamePL", "ProjectWebsiteUrl" },
                values: new object[,]
                {
                    { 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida felis in ultrices molestie.", "PL Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida felis in ultrices molestie.", "git", null, "Project#1", "Projekt#1", null },
                    { 2, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida felis in ultrices molestie.", "PL Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida felis in ultrices molestie.", null, null, "Project#2", "Projekt#2", "git" },
                    { 3, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida felis in ultrices molestie.", "PL Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida felis in ultrices molestie.", "git", null, "Project#3", "Projekt#3", "git" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "ImageUrl", "NameENG", "NamePL" },
                values: new object[,]
                {
                    { 1, null, "C#", "PL C#" },
                    { 2, null, "C#2", "PL C#2" },
                    { 3, null, "C#3", "PL C#3" },
                    { 4, null, "C#4", "PL C#4" },
                    { 5, null, "C#5", "PL C#5" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "EmailMessages");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Skills");
        }
    }
}
