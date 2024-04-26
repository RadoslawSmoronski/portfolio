using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace portfolio.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectsTableToDbAndSeedProjectsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida felis in ultrices molestie.");

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida felis in ultrices molestie.");

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida felis in ultrices molestie.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Projects");
        }
    }
}
