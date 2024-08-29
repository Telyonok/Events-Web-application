using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthorizationServer.Migrations
{
    /// <inheritdoc />
    public partial class AddedRefreshTokensId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_User_AppUserId",
                table: "RefreshToken");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "RefreshToken",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_User_AppUserId",
                table: "RefreshToken",
                column: "AppUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_User_AppUserId",
                table: "RefreshToken");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "RefreshToken",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_User_AppUserId",
                table: "RefreshToken",
                column: "AppUserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
