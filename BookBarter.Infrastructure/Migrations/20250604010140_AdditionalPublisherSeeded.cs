using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookBarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalPublisherSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Global Publishers" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
