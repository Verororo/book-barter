using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookBarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BookColsAdjusted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicationDate",
                value: new DateOnly(2008, 12, 2));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublicationDate",
                value: new DateOnly(2012, 9, 26));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicationDate",
                value: new DateOnly(1, 1, 1));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublicationDate",
                value: new DateOnly(1, 1, 1));
        }
    }
}
