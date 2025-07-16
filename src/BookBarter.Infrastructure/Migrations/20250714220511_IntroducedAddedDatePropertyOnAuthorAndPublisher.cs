using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookBarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IntroducedAddedDatePropertyOnAuthorAndPublisher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddedToDatabaseDate",
                table: "Books",
                newName: "AddedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "Publishers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "Authors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "AddedDate",
                table: "Books",
                newName: "AddedToDatabaseDate");
        }
    }
}
