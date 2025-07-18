using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookBarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedWantedBookEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WantedBooks_AspNetUsers_WantedByUsersId",
                table: "WantedBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_WantedBooks_Books_WantedBooksId",
                table: "WantedBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WantedBooks",
                table: "WantedBooks");

            migrationBuilder.DropIndex(
                name: "IX_WantedBooks_WantedByUsersId",
                table: "WantedBooks");

            migrationBuilder.RenameColumn(
                name: "WantedByUsersId",
                table: "WantedBooks",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "WantedBooksId",
                table: "WantedBooks",
                newName: "BookStateId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "WantedBooks",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "WantedBooks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "WantedBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Publishers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "OwnedBooks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Isbn",
                table: "Books",
                type: "nchar(13)",
                fixedLength: true,
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Authors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WantedBooks",
                table: "WantedBooks",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "Approved",
                value: false);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                column: "Approved",
                value: false);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Approved",
                value: false);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Approved",
                value: false);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Approved",
                value: false);

            migrationBuilder.CreateIndex(
                name: "IX_WantedBooks_BookId",
                table: "WantedBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_WantedBooks_UserId_BookId",
                table: "WantedBooks",
                columns: new[] { "UserId", "BookId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WantedBooks_AspNetUsers_UserId",
                table: "WantedBooks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WantedBooks_Books_BookId",
                table: "WantedBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WantedBooks_AspNetUsers_UserId",
                table: "WantedBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_WantedBooks_Books_BookId",
                table: "WantedBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WantedBooks",
                table: "WantedBooks");

            migrationBuilder.DropIndex(
                name: "IX_WantedBooks_BookId",
                table: "WantedBooks");

            migrationBuilder.DropIndex(
                name: "IX_WantedBooks_UserId_BookId",
                table: "WantedBooks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "WantedBooks");

            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "WantedBooks");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "WantedBooks");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "OwnedBooks");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "WantedBooks",
                newName: "WantedByUsersId");

            migrationBuilder.RenameColumn(
                name: "BookStateId",
                table: "WantedBooks",
                newName: "WantedBooksId");

            migrationBuilder.AlterColumn<string>(
                name: "Isbn",
                table: "Books",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(13)",
                oldFixedLength: true,
                oldMaxLength: 13);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WantedBooks",
                table: "WantedBooks",
                columns: new[] { "WantedBooksId", "WantedByUsersId" });

            migrationBuilder.CreateIndex(
                name: "IX_WantedBooks_WantedByUsersId",
                table: "WantedBooks",
                column: "WantedByUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_WantedBooks_AspNetUsers_WantedByUsersId",
                table: "WantedBooks",
                column: "WantedByUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WantedBooks_Books_WantedBooksId",
                table: "WantedBooks",
                column: "WantedBooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
