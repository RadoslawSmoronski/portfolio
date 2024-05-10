using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace portfolio.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddIsReadedColumnToEmailMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReaded",
                table: "EmailMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "EmailMessages",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsReaded",
                value: false);

            migrationBuilder.UpdateData(
                table: "EmailMessages",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsReaded",
                value: false);

            migrationBuilder.UpdateData(
                table: "EmailMessages",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsReaded",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReaded",
                table: "EmailMessages");
        }
    }
}
