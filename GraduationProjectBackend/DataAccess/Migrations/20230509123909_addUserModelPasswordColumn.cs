using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProjectBackend.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addUserModelPasswordColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "password",
                table: "User",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password",
                table: "User");
        }
    }
}
