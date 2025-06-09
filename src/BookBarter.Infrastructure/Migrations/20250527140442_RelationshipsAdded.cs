using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookBarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RelationshipsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.CreateTable(
                name: "AuthorBook",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "int", nullable: false),
                    BooksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBook", x => new { x.AuthorsId, x.BooksId });
                    table.ForeignKey(
                        name: "FK_AuthorBook_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBook_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookstates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookstates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserWantsBook",
                columns: table => new
                {
                    BooksWantedId = table.Column<int>(type: "int", nullable: false),
                    UsersWantedById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWantsBook", x => new { x.BooksWantedId, x.UsersWantedById });
                    table.ForeignKey(
                        name: "FK_UserWantsBook_Books_BooksWantedId",
                        column: x => x.BooksWantedId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWantsBook_Users_UsersWantedById",
                        column: x => x.UsersWantedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bookstates",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Old" },
                    { 2, "Medium" },
                    { 3, "New" }
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

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreId",
                table: "Books",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_BooksId",
                table: "AuthorBook",
                column: "BooksId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWantsBook_UsersWantedById",
                table: "UserWantsBook",
                column: "UsersWantedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Genres_GenreId",
                table: "Books",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id");

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
                name: "FK_UserHasBooks_Books_BookId",
                table: "UserHasBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserHasBooks_Bookstates_BookStateId",
                table: "UserHasBooks",
                column: "BookStateId",
                principalTable: "Bookstates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserHasBooks_Users_UserId",
                table: "UserHasBooks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Genres_GenreId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_ReceiverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserHasBooks_Books_BookId",
                table: "UserHasBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserHasBooks_Bookstates_BookStateId",
                table: "UserHasBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserHasBooks_Users_UserId",
                table: "UserHasBooks");

            migrationBuilder.DropTable(
                name: "AuthorBook");

            migrationBuilder.DropTable(
                name: "Bookstates");

            migrationBuilder.DropTable(
                name: "UserWantsBook");

            migrationBuilder.DropIndex(
                name: "IX_UserHasBooks_BookId",
                table: "UserHasBooks");

            migrationBuilder.DropIndex(
                name: "IX_UserHasBooks_BookStateId",
                table: "UserHasBooks");

            migrationBuilder.DropIndex(
                name: "IX_UserHasBooks_UserId",
                table: "UserHasBooks");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_SenderId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Books_GenreId",
                table: "Books");

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Old" },
                    { 2, "Medium" },
                    { 3, "New" }
                });
        }
    }
}
