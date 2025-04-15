using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PlaygroundDotNetAPI.Migrations
{
    /// <inheritdoc />
    public partial class CharmanderLine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pokedex",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Charmander" },
                    { 5, "Charmeleon" },
                    { 6, "Charizard" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pokedex",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Pokedex",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Pokedex",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
