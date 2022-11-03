using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Migrations
{
    public partial class Add_Role_Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "DateCreated", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "admin", "2848dc53-63b8-42c1-ae66-3be4d7406d08", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", null },
                    { "client", "cdc12ca1-3a21-45d0-9df7-2fbb69bf5c08", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Client", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "client");
        }
    }
}
