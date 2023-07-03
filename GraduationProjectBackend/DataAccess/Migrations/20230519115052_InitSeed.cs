﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GraduationProjectBackend.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteFolder_User_userId",
                table: "FavoriteFolder");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteFolderItem_FavoriteFolder_favoriteFolderId",
                table: "FavoriteFolderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteFolderItem_FavoriteItem_favoriteItemId",
                table: "FavoriteFolderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_roleId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_userId",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_userId",
                table: "UserRole");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "UserRole",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "roleId",
                table: "UserRole",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "userRoleId",
                table: "UserRole",
                newName: "UserRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_roleId",
                table: "UserRole",
                newName: "IX_UserRole_RoleId");

            migrationBuilder.RenameColumn(
                name: "favoriteItemName",
                table: "FavoriteItem",
                newName: "FavoriteItemName");

            migrationBuilder.RenameColumn(
                name: "favoriteItemId",
                table: "FavoriteItem",
                newName: "FavoriteItemId");

            migrationBuilder.RenameColumn(
                name: "favoriteItemId",
                table: "FavoriteFolderItem",
                newName: "FavoriteItemId");

            migrationBuilder.RenameColumn(
                name: "favoriteFolderId",
                table: "FavoriteFolderItem",
                newName: "FavoriteFolderId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteFolderItem_favoriteItemId",
                table: "FavoriteFolderItem",
                newName: "IX_FavoriteFolderItem_FavoriteItemId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteFolderItem_favoriteFolderId",
                table: "FavoriteFolderItem",
                newName: "IX_FavoriteFolderItem_FavoriteFolderId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "FavoriteFolder",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "favoriteFolderName",
                table: "FavoriteFolder",
                newName: "FavoriteFolderName");

            migrationBuilder.RenameColumn(
                name: "favoriteFolderId",
                table: "FavoriteFolder",
                newName: "FavoriteFolderId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteFolder_userId",
                table: "FavoriteFolder",
                newName: "IX_FavoriteFolder_UserId");

            migrationBuilder.AddColumn<int>(
                name: "FavoriteFolderId",
                table: "UserRole",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "roleName",
                table: "Role",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FavoriteItemId",
                table: "FavoriteFolderItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FavoriteFolderId",
                table: "FavoriteFolderItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "FavoriteFolder",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "FavoriteItem",
                columns: new[] { "FavoriteItemId", "FavoriteItemName" },
                values: new object[,]
                {
                    { 1, "館長" },
                    { 2, "廖老大" },
                    { 3, "總統" },
                    { 4, "蔡英文" },
                    { 5, "選舉" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "roleId", "roleName" },
                values: new object[,]
                {
                    { 1, "Users" },
                    { 2, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "userId", "account", "password" },
                values: new object[,]
                {
                    { 10000, "user1", new byte[] { 93, 215, 213, 162, 83, 107, 157, 171, 27, 228, 95, 112, 165, 141, 154, 108, 230, 32, 146, 34, 97, 206, 40, 251, 208, 5, 106, 60, 221, 127, 85, 238, 159, 84, 196, 217, 181, 182, 127, 137, 132, 186, 131, 125, 70, 225, 51, 48, 205, 73, 139, 9, 35, 8, 165, 233, 255, 140, 36, 78, 189, 209, 101, 108, 7, 50, 229, 93, 127, 119, 58, 2, 15, 118, 129, 63, 72, 240, 112, 193, 33, 23, 129, 37, 198, 15, 95, 80, 170, 14, 84, 49, 72, 250, 221, 153, 24, 69, 167, 3, 166, 197, 50, 215, 173, 160, 105, 95, 89, 106, 144, 189, 56, 171, 255, 187, 242, 19, 227, 189, 169, 247, 209, 189, 26, 57, 120, 131, 176, 13, 92, 236, 16, 13, 197, 41, 172, 26, 222, 82, 11, 212, 189, 140, 214, 109, 70, 173, 189, 208, 96, 112, 26, 92, 185, 245, 60, 181, 112, 149, 125, 149, 47, 198, 5, 24, 31, 220, 67, 159, 51, 201, 193, 30, 138, 164, 131, 86, 2, 159, 120, 100, 252, 136, 52, 187, 116, 246, 6, 74, 229, 87, 125, 211, 233, 212, 158, 156, 253, 86, 252, 143, 74, 98, 50, 173, 242, 114, 89, 81, 71, 137, 17, 66, 95, 202, 185, 224, 97, 162, 218, 159, 183, 102, 12, 32, 240, 14, 229, 47, 75, 127, 113, 218, 27, 24, 152, 17, 11, 154, 131, 55, 92, 125, 146, 41, 136, 115, 31, 214, 61, 231, 213, 120, 207, 31 } },
                    { 10001, "user2", new byte[] { 105, 114, 247, 200, 181, 26, 7, 225, 35, 68, 40, 55, 47, 4, 103, 143, 236, 86, 179, 242, 24, 69, 248, 190, 153, 177, 51, 162, 232, 118, 192, 28, 232, 141, 61, 73, 249, 92, 22, 133, 98, 240, 204, 141, 66, 120, 212, 60, 29, 117, 132, 115, 120, 108, 7, 116, 112, 211, 195, 94, 202, 73, 73, 97, 198, 38, 106, 65, 135, 14, 74, 232, 225, 88, 81, 25, 76, 159, 63, 35, 145, 172, 74, 4, 99, 41, 149, 16, 110, 208, 211, 207, 30, 178, 213, 244, 34, 126, 192, 237, 197, 34, 213, 94, 212, 232, 35, 227, 138, 13, 135, 237, 170, 17, 14, 11, 36, 42, 61, 253, 190, 148, 2, 74, 94, 70, 203, 255, 123, 198, 127, 137, 215, 190, 222, 186, 106, 173, 209, 73, 207, 99, 234, 117, 182, 13, 8, 246, 222, 39, 85, 224, 191, 14, 12, 183, 161, 248, 224, 61, 110, 223, 52, 136, 238, 179, 81, 135, 201, 79, 111, 69, 191, 36, 160, 247, 170, 150, 63, 183, 99, 249, 166, 164, 226, 164, 251, 224, 195, 126, 85, 208, 152, 43, 228, 46, 228, 133, 244, 27, 40, 153, 13, 59, 143, 67, 41, 174, 250, 221, 48, 23, 7, 139, 83, 68, 31, 65, 139, 242, 108, 166, 87, 206, 239, 48, 58, 10, 126, 91, 187, 7, 6, 37, 191, 78, 184, 90, 118, 140, 96, 207, 136, 95, 219, 136, 121, 216, 236, 69, 154, 114, 32, 95, 125, 196 } },
                    { 10002, "user3", new byte[] { 11, 48, 7, 218, 29, 51, 132, 183, 51, 225, 155, 58, 139, 77, 15, 117, 27, 63, 112, 187, 187, 202, 90, 135, 236, 65, 24, 110, 212, 153, 240, 124, 63, 34, 103, 218, 25, 150, 107, 94, 72, 169, 245, 133, 234, 115, 167, 124, 186, 158, 242, 3, 145, 34, 104, 229, 46, 213, 150, 164, 132, 127, 128, 251, 75, 169, 246, 154, 125, 130, 46, 219, 30, 90, 244, 213, 232, 96, 34, 18, 172, 103, 180, 39, 79, 73, 192, 178, 115, 78, 20, 140, 217, 33, 227, 146, 0, 39, 69, 139, 239, 63, 97, 106, 47, 188, 192, 29, 134, 253, 192, 8, 94, 50, 117, 56, 139, 36, 1, 114, 178, 102, 48, 90, 37, 31, 60, 182, 40, 207, 14, 232, 103, 87, 247, 252, 102, 216, 31, 63, 129, 109, 62, 174, 155, 26, 171, 144, 177, 9, 107, 232, 135, 33, 24, 182, 85, 174, 137, 4, 120, 96, 37, 27, 74, 184, 192, 131, 32, 153, 60, 4, 76, 66, 106, 185, 196, 0, 232, 192, 1, 217, 36, 100, 250, 12, 199, 3, 12, 180, 2, 238, 137, 34, 149, 233, 253, 236, 232, 0, 36, 96, 143, 191, 129, 25, 203, 131, 115, 47, 218, 102, 32, 228, 17, 120, 143, 50, 49, 3, 46, 203, 225, 117, 15, 228, 206, 186, 37, 44, 153, 58, 11, 194, 237, 43, 54, 57, 40, 158, 72, 220, 251, 173, 52, 254, 114, 196, 249, 210, 46, 4, 226, 93, 3, 124 } }
                });

            migrationBuilder.InsertData(
                table: "FavoriteFolder",
                columns: new[] { "FavoriteFolderId", "FavoriteFolderName", "UserId" },
                values: new object[,]
                {
                    { 1, "資料夾一", 10000 },
                    { 2, "資料夾二", 10000 },
                    { 3, "資料夾三", 10000 },
                    { 4, "資料夾一", 10001 },
                    { 5, "資料夾二", 10001 }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleId", "FavoriteFolderId", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, null, 1, 10000 },
                    { 2, null, 1, 10001 },
                    { 3, null, 1, 10002 }
                });

            migrationBuilder.InsertData(
                table: "FavoriteFolderItem",
                columns: new[] { "favoriteFolderItemId", "FavoriteFolderId", "FavoriteItemId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 },
                    { 4, 2, 2 },
                    { 5, 2, 3 },
                    { 6, 3, 4 },
                    { 7, 3, 5 },
                    { 8, 4, 1 },
                    { 9, 5, 4 },
                    { 10, 5, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_FavoriteFolderId",
                table: "UserRole",
                column: "FavoriteFolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteFolder_User_UserId",
                table: "FavoriteFolder",
                column: "UserId",
                principalTable: "User",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteFolderItem_FavoriteFolder_FavoriteFolderId",
                table: "FavoriteFolderItem",
                column: "FavoriteFolderId",
                principalTable: "FavoriteFolder",
                principalColumn: "FavoriteFolderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteFolderItem_FavoriteItem_FavoriteItemId",
                table: "FavoriteFolderItem",
                column: "FavoriteItemId",
                principalTable: "FavoriteItem",
                principalColumn: "FavoriteItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleId",
                table: "UserRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "roleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_FavoriteFolderId",
                table: "UserRole",
                column: "FavoriteFolderId",
                principalTable: "User",
                principalColumn: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteFolder_User_UserId",
                table: "FavoriteFolder");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteFolderItem_FavoriteFolder_FavoriteFolderId",
                table: "FavoriteFolderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteFolderItem_FavoriteItem_FavoriteItemId",
                table: "FavoriteFolderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_RoleId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_FavoriteFolderId",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_FavoriteFolderId",
                table: "UserRole");

            migrationBuilder.DeleteData(
                table: "FavoriteFolderItem",
                keyColumn: "favoriteFolderItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FavoriteFolderItem",
                keyColumn: "favoriteFolderItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FavoriteFolderItem",
                keyColumn: "favoriteFolderItemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FavoriteFolderItem",
                keyColumn: "favoriteFolderItemId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FavoriteFolderItem",
                keyColumn: "favoriteFolderItemId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FavoriteFolderItem",
                keyColumn: "favoriteFolderItemId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "FavoriteFolderItem",
                keyColumn: "favoriteFolderItemId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "FavoriteFolderItem",
                keyColumn: "favoriteFolderItemId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "FavoriteFolderItem",
                keyColumn: "favoriteFolderItemId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "FavoriteFolderItem",
                keyColumn: "favoriteFolderItemId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "roleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "userId",
                keyValue: 10002);

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FavoriteFolder",
                keyColumn: "FavoriteFolderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FavoriteFolder",
                keyColumn: "FavoriteFolderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FavoriteFolder",
                keyColumn: "FavoriteFolderId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FavoriteFolder",
                keyColumn: "FavoriteFolderId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FavoriteFolder",
                keyColumn: "FavoriteFolderId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FavoriteItem",
                keyColumn: "FavoriteItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FavoriteItem",
                keyColumn: "FavoriteItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FavoriteItem",
                keyColumn: "FavoriteItemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FavoriteItem",
                keyColumn: "FavoriteItemId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FavoriteItem",
                keyColumn: "FavoriteItemId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "roleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "userId",
                keyValue: 10000);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "userId",
                keyValue: 10001);

            migrationBuilder.DropColumn(
                name: "FavoriteFolderId",
                table: "UserRole");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserRole",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "UserRole",
                newName: "roleId");

            migrationBuilder.RenameColumn(
                name: "UserRoleId",
                table: "UserRole",
                newName: "userRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                newName: "IX_UserRole_roleId");

            migrationBuilder.RenameColumn(
                name: "FavoriteItemName",
                table: "FavoriteItem",
                newName: "favoriteItemName");

            migrationBuilder.RenameColumn(
                name: "FavoriteItemId",
                table: "FavoriteItem",
                newName: "favoriteItemId");

            migrationBuilder.RenameColumn(
                name: "FavoriteItemId",
                table: "FavoriteFolderItem",
                newName: "favoriteItemId");

            migrationBuilder.RenameColumn(
                name: "FavoriteFolderId",
                table: "FavoriteFolderItem",
                newName: "favoriteFolderId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteFolderItem_FavoriteItemId",
                table: "FavoriteFolderItem",
                newName: "IX_FavoriteFolderItem_favoriteItemId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteFolderItem_FavoriteFolderId",
                table: "FavoriteFolderItem",
                newName: "IX_FavoriteFolderItem_favoriteFolderId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FavoriteFolder",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "FavoriteFolderName",
                table: "FavoriteFolder",
                newName: "favoriteFolderName");

            migrationBuilder.RenameColumn(
                name: "FavoriteFolderId",
                table: "FavoriteFolder",
                newName: "favoriteFolderId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteFolder_UserId",
                table: "FavoriteFolder",
                newName: "IX_FavoriteFolder_userId");

            migrationBuilder.AlterColumn<string>(
                name: "roleName",
                table: "Role",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "favoriteItemId",
                table: "FavoriteFolderItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "favoriteFolderId",
                table: "FavoriteFolderItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "userId",
                table: "FavoriteFolder",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_userId",
                table: "UserRole",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteFolder_User_userId",
                table: "FavoriteFolder",
                column: "userId",
                principalTable: "User",
                principalColumn: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteFolderItem_FavoriteFolder_favoriteFolderId",
                table: "FavoriteFolderItem",
                column: "favoriteFolderId",
                principalTable: "FavoriteFolder",
                principalColumn: "favoriteFolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteFolderItem_FavoriteItem_favoriteItemId",
                table: "FavoriteFolderItem",
                column: "favoriteItemId",
                principalTable: "FavoriteItem",
                principalColumn: "favoriteItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_roleId",
                table: "UserRole",
                column: "roleId",
                principalTable: "Role",
                principalColumn: "roleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_userId",
                table: "UserRole",
                column: "userId",
                principalTable: "User",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
