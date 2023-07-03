using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProjectBackend.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fixFavorite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_User_userId",
                table: "Role");

            migrationBuilder.DropTable(
                name: "UserFavoriteFolder");

            migrationBuilder.DropIndex(
                name: "IX_Role_userId",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Role");

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "FavoriteFolder",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteFolder_userId",
                table: "FavoriteFolder",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteFolder_User_userId",
                table: "FavoriteFolder",
                column: "userId",
                principalTable: "User",
                principalColumn: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteFolder_User_userId",
                table: "FavoriteFolder");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteFolder_userId",
                table: "FavoriteFolder");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "FavoriteFolder");

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Role",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserFavoriteFolder",
                columns: table => new
                {
                    userFavoriteFolderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    favoriteFolderId = table.Column<int>(type: "int", nullable: true),
                    userId = table.Column<int>(type: "int", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_Role_userId",
                table: "Role",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteFolder_favoriteFolderId",
                table: "UserFavoriteFolder",
                column: "favoriteFolderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteFolder_userId",
                table: "UserFavoriteFolder",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_User_userId",
                table: "Role",
                column: "userId",
                principalTable: "User",
                principalColumn: "userId");
        }
    }
}
