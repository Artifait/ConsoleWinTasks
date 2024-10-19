using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleWinTasks.Migrations
{
    /// <inheritdoc />
    public partial class AddStudioFieldToGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Studio",
                table: "Game",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Studio",
                table: "Game");
        }
    }
}
