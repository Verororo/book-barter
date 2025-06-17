using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookBarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BookAddedToDatabaseDatePropertyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddedToDatabaseDate",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddedToDatabaseDate",
                value: new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddedToDatabaseDate",
                value: new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedToDatabaseDate",
                table: "Books");
        }
    }
}
