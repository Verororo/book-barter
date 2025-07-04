using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookBarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedBookStateFromWantedBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookStateId",
                table: "WantedBooks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookStateId",
                table: "WantedBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
