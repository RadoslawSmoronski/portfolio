using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace portfolio.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddEngAndPlNamesDescriptionsAndProjectWebisteUrlColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Projects",
                newName: "NamePL");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Projects",
                newName: "DescriptionPL");

            migrationBuilder.AlterColumn<string>(
                name: "GitRepositoryUrl",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionENG",
                table: "Projects",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameENG",
                table: "Projects",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectWebsiteUrl",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DescriptionENG", "DescriptionPL", "NameENG", "NamePL", "ProjectWebsiteUrl" },
                values: new object[] { "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida felis in ultrices molestie.", "PL Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida felis in ultrices molestie.", "Project#1", "Projekt#1", null });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DescriptionENG", "DescriptionPL", "GitRepositoryUrl", "NameENG", "NamePL", "ProjectWebsiteUrl" },
                values: new object[] { "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida felis in ultrices molestie.", "PL Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida felis in ultrices molestie.", null, "Project#2", "Projekt#2", "git" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DescriptionENG", "DescriptionPL", "NameENG", "NamePL", "ProjectWebsiteUrl" },
                values: new object[] { "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida felis in ultrices molestie.", "PL Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida felis in ultrices molestie.", "Project#3", "Projekt#3", "git" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionENG",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "NameENG",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectWebsiteUrl",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "NamePL",
                table: "Projects",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DescriptionPL",
                table: "Projects",
                newName: "Description");

            migrationBuilder.AlterColumn<string>(
                name: "GitRepositoryUrl",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida felis in ultrices molestie.", "Project#1" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "GitRepositoryUrl", "Name" },
                values: new object[] { "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida felis in ultrices molestie.", "git", "Project#2" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida felis in ultrices molestie.", "Project#3" });
        }
    }
}
