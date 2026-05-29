using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodePulse.API.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class CommunityVersionInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "37c35ff3-3d62-4dce-9eaa-ad3e278c7b4e", "836ece39-23c0-4dbe-a56a-b02ea446200f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37c35ff3-3d62-4dce-9eaa-ad3e278c7b4e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cff7c87a-48c5-4967-a51f-6402cf408546",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Admin", "ADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cff7c87a-48c5-4967-a51f-6402cf408546",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Reader", "READER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "37c35ff3-3d62-4dce-9eaa-ad3e278c7b4e", "37c35ff3-3d62-4dce-9eaa-ad3e278c7b4e", "Writer", "WRITER" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "37c35ff3-3d62-4dce-9eaa-ad3e278c7b4e", "836ece39-23c0-4dbe-a56a-b02ea446200f" });
        }
    }
}
