using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Migrations
{
    public partial class Add_NomalizeName_Role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "c7d204e1-aece-4c51-a1d3-a16b2e7bfe3b", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "client",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "f79ee26c-d198-45fd-b0f3-1b7c5e070d4b", "CLIENT" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "a05e9448-f264-422b-b8b8-63473ec6ce4a", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "client",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "a1fa00ee-95ef-4225-8fab-ad52a9f21f32", null });
        }
    }
}
