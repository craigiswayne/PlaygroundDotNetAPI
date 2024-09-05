using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PlaygroundDotNetAPI.Migrations
{
    /// <inheritdoc />
    public partial class SquirtleLine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pokedex",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 7, "Squirtle" },
                    { 8, "Wartortle" },
                    { 9, "Blastoise" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pokedex",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Pokedex",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Pokedex",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
