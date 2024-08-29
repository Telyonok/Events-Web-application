using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthorizationServer.Migrations
{
    /// <inheritdoc />
    public partial class AddedRefreshTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "e0dd19cf-ab64-40cf-adb3-2ea3bf5cb9cf" });

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "e0dd19cf-ab64-40cf-adb3-2ea3bf5cb9cf");

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TokenString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "60ca4504-70b4-4f67-98cd-3b5f3d0a6a34", 0, "5ffbfaa6-41c2-4611-a1b8-f711473cc6f4", null, false, "Ilya", "Chvilyov", false, null, null, "ADMINISTRATOR", "AQAAAAIAAYagAAAAEEEN2VuOPy1JZtmpQnRghCbta5V7erPq24Df58DPZzFABHSTsiFIepWLXrAMm1pG7w==", null, false, "UNUYCBKBTG7CYWXZEJ67XU5EOKY5ETZZ", false, "Administrator" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "60ca4504-70b4-4f67-98cd-3b5f3d0a6a34" });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_AppUserId",
                table: "RefreshToken",
                column: "AppUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "60ca4504-70b4-4f67-98cd-3b5f3d0a6a34" });

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "60ca4504-70b4-4f67-98cd-3b5f3d0a6a34");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e0dd19cf-ab64-40cf-adb3-2ea3bf5cb9cf", 0, "2edbdd23-b51f-4a36-a5c3-f046e3d068ab", null, false, "Ilya", "Chvilyov", false, null, null, "TELYONOK", "AQAAAAEAACcQAAAAEPW/RDjSyUSGj5CVaa3nKdnb+fQBUoDeIwN5higGf8JMl7ik9pqyat3v60PTYzND8w==", null, false, "MBWOCQLR2CMJH63BYU6SDWSAPXMTEYIC", false, "Telyonok" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "e0dd19cf-ab64-40cf-adb3-2ea3bf5cb9cf" });
        }
    }
}
