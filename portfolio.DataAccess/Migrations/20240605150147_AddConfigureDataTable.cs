using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace portfolio.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigureDataTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfigureDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JSON = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigureDatas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ConfigureDatas",
                columns: new[] { "Id", "JSON" },
                values: new object[] { 1, "{\"HashedPassword\":\"$2a$11$8WGPCFiXVzavlpu6KaqakO738nLjnUrvioepPN0VwnQ3SD6SZZKUS\"}" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigureDatas");
        }
    }
}
