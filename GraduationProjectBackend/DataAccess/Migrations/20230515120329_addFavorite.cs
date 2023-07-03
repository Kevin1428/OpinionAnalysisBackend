using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProjectBackend.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addFavorite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavoriteFolder",
                columns: table => new
                {
                    favoriteFolderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    favoriteFolderName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteFolder", x => x.favoriteFolderId);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteItem",
                columns: table => new
                {
                    favoriteItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    favoriteItemName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteItem", x => x.favoriteItemId);
                });

            migrationBuilder.CreateTable(
                name: "UserFavoriteFolder",
                columns: table => new
                {
                    userFavoriteFolderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: true),
                    favoriteFolderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteFolder", x => x.userFavoriteFolderId);
                    table.ForeignKey(
                        name: "FK_UserFavoriteFolder_FavoriteFolder_favoriteFolderId",
                        column: x => x.favoriteFolderId,
                        principalTable: "FavoriteFolder",
                        principalColumn: "favoriteFolderId");
                    table.ForeignKey(
                        name: "FK_UserFavoriteFolder_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "FavoriteFolderItem",
                columns: table => new
                {
                    favoriteFolderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    favoriteFolderId = table.Column<int>(type: "int", nullable: true),
                    favoriteItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteFolderItem", x => x.favoriteFolderItemId);
                    table.ForeignKey(
                        name: "FK_FavoriteFolderItem_FavoriteFolder_favoriteFolderId",
                        column: x => x.favoriteFolderId,
                        principalTable: "FavoriteFolder",
                        principalColumn: "favoriteFolderId");
                    table.ForeignKey(
                        name: "FK_FavoriteFolderItem_FavoriteItem_favoriteItemId",
                        column: x => x.favoriteItemId,
                        principalTable: "FavoriteItem",
                        principalColumn: "favoriteItemId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteFolderItem_favoriteFolderId",
                table: "FavoriteFolderItem",
                column: "favoriteFolderId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteFolderItem_favoriteItemId",
                table: "FavoriteFolderItem",
                column: "favoriteItemId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteFolder_favoriteFolderId",
                table: "UserFavoriteFolder",
                column: "favoriteFolderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteFolder_userId",
                table: "UserFavoriteFolder",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteFolderItem");

            migrationBuilder.DropTable(
                name: "UserFavoriteFolder");

            migrationBuilder.DropTable(
                name: "FavoriteItem");

            migrationBuilder.DropTable(
                name: "FavoriteFolder");
        }
    }
}
