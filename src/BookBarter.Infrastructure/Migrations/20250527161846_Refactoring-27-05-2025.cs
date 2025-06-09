using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookBarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Refactoring27052025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_ReceiverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserWantsBook_Books_BooksWantedId",
                table: "UserWantsBook");

            migrationBuilder.DropForeignKey(
                name: "FK_UserWantsBook_Users_UsersWantedById",
                table: "UserWantsBook");

            migrationBuilder.DropTable(
                name: "UserHasBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookstates",
                table: "Bookstates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserWantsBook",
                table: "UserWantsBook");

            migrationBuilder.RenameTable(
                name: "Bookstates",
                newName: "BookStates");

            migrationBuilder.RenameTable(
                name: "UserWantsBook",
                newName: "WantedBooks");

            migrationBuilder.RenameColumn(
                name: "UsersWantedById",
                table: "WantedBooks",
                newName: "WantedByUsersId");

            migrationBuilder.RenameColumn(
                name: "BooksWantedId",
                table: "WantedBooks",
                newName: "WantedBooksId");

            migrationBuilder.RenameIndex(
                name: "IX_UserWantsBook_UsersWantedById",
                table: "WantedBooks",
                newName: "IX_WantedBooks_WantedByUsersId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<bool>(
                name: "Approved",
                table: "Books",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookStates",
                table: "BookStates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WantedBooks",
                table: "WantedBooks",
                columns: new[] { "WantedBooksId", "WantedByUsersId" });

            migrationBuilder.CreateTable(
                name: "OwnedBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookStateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnedBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OwnedBooks_BookStates_BookStateId",
                        column: x => x.BookStateId,
                        principalTable: "BookStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OwnedBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OwnedBooks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Name",
                table: "Genres",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_Isbn",
                table: "Books",
                column: "Isbn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OwnedBooks_BookId",
                table: "OwnedBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnedBooks_BookStateId",
                table: "OwnedBooks",
                column: "BookStateId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnedBooks_UserId_BookId",
                table: "OwnedBooks",
                columns: new[] { "UserId", "BookId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WantedBooks_Books_WantedBooksId",
                table: "WantedBooks",
                column: "WantedBooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WantedBooks_Users_WantedByUsersId",
                table: "WantedBooks",
                column: "WantedByUsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_ReceiverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_WantedBooks_Books_WantedBooksId",
                table: "WantedBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_WantedBooks_Users_WantedByUsersId",
                table: "WantedBooks");

            migrationBuilder.DropTable(
                name: "OwnedBooks");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Genres_Name",
                table: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookStates",
                table: "BookStates");

            migrationBuilder.DropIndex(
                name: "IX_Books_Isbn",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WantedBooks",
                table: "WantedBooks");

            migrationBuilder.RenameTable(
                name: "BookStates",
                newName: "Bookstates");

            migrationBuilder.RenameTable(
                name: "WantedBooks",
                newName: "UserWantsBook");

            migrationBuilder.RenameColumn(
                name: "WantedByUsersId",
                table: "UserWantsBook",
                newName: "UsersWantedById");

            migrationBuilder.RenameColumn(
                name: "WantedBooksId",
                table: "UserWantsBook",
                newName: "BooksWantedId");

            migrationBuilder.RenameIndex(
                name: "IX_WantedBooks_WantedByUsersId",
                table: "UserWantsBook",
                newName: "IX_UserWantsBook_UsersWantedById");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<bool>(
                name: "Approved",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookstates",
                table: "Bookstates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserWantsBook",
                table: "UserWantsBook",
                columns: new[] { "BooksWantedId", "UsersWantedById" });

            migrationBuilder.CreateTable(
                name: "UserHasBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    BookStateId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHasBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserHasBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserHasBooks_Bookstates_BookStateId",
                        column: x => x.BookStateId,
                        principalTable: "Bookstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserHasBooks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserHasBooks_BookId",
                table: "UserHasBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHasBooks_BookStateId",
                table: "UserHasBooks",
                column: "BookStateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHasBooks_UserId",
                table: "UserHasBooks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserWantsBook_Books_BooksWantedId",
                table: "UserWantsBook",
                column: "BooksWantedId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserWantsBook_Users_UsersWantedById",
                table: "UserWantsBook",
                column: "UsersWantedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
