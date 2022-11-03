using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Migrations
{
    public partial class Update_UserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin",
                column: "ConcurrencyStamp",
                value: "a05e9448-f264-422b-b8b8-63473ec6ce4a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "client",
                column: "ConcurrencyStamp",
                value: "a1fa00ee-95ef-4225-8fab-ad52a9f21f32");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin",
                column: "ConcurrencyStamp",
                value: "2848dc53-63b8-42c1-ae66-3be4d7406d08");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "client",
                column: "ConcurrencyStamp",
                value: "cdc12ca1-3a21-45d0-9df7-2fbb69bf5c08");
        }
    }
}
