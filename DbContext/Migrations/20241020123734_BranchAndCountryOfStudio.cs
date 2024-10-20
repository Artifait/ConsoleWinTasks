using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleWinTasks.Migrations
{
    /// <inheritdoc />
    public partial class BranchAndCountryOfStudio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Studio",
                table: "Game");

            migrationBuilder.AlterColumn<string>(
                name: "GameMode",
                table: "Game",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudioId",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Studio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudioBranch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StudioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudioBranch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudioBranch_Studio_StudioId",
                        column: x => x.StudioId,
                        principalTable: "Studio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudioCountry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StudioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudioCountry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudioCountry_Studio_StudioId",
                        column: x => x.StudioId,
                        principalTable: "Studio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_StudioId",
                table: "Game",
                column: "StudioId");

            migrationBuilder.CreateIndex(
                name: "IX_StudioBranch_StudioId",
                table: "StudioBranch",
                column: "StudioId");

            migrationBuilder.CreateIndex(
                name: "IX_StudioCountry_StudioId",
                table: "StudioCountry",
                column: "StudioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Studio",
                table: "Game",
                column: "StudioId",
                principalTable: "Studio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Studio",
                table: "Game");

            migrationBuilder.DropTable(
                name: "StudioBranch");

            migrationBuilder.DropTable(
                name: "StudioCountry");

            migrationBuilder.DropTable(
                name: "Studio");

            migrationBuilder.DropIndex(
                name: "IX_Game_StudioId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "StudioId",
                table: "Game");

            migrationBuilder.AlterColumn<string>(
                name: "GameMode",
                table: "Game",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Studio",
                table: "Game",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
