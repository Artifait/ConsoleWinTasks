using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleWinTasks.Migrations
{
    /// <inheritdoc />
    public partial class PreviousBookId_NotSelfReferencing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Book_PreviousBookId_NotSelfReferencing",
                table: "Books",
                sql: "[PreviousBookId] IS NULL OR [PreviousBookId] <> [Id]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Book_PreviousBookId_NotSelfReferencing",
                table: "Books");
        }
    }
}
