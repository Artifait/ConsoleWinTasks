using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleWinTasks.Migrations
{
    /// <inheritdoc />
    public partial class AddGameModeAndCopiesSold : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CopiesSold",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GameMode",
                table: "Game",
                type: "nvarchar(200)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CopiesSold",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "GameMode",
                table: "Game");
        }
    }
}
