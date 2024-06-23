using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class ImageUrlPng : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00e66373-89a9-4b28-872e-5abc909350d3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1563f100-3d96-4fb1-b517-8b135d900b61");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "43735104-a0a2-4a15-b45d-a7c9ddfa8b3b", null, "Admin", "ADMIN" },
                    { "fcea5b1b-29f3-48c7-8797-0033ae831fdc", null, "User", "USER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43735104-a0a2-4a15-b45d-a7c9ddfa8b3b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fcea5b1b-29f3-48c7-8797-0033ae831fdc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "00e66373-89a9-4b28-872e-5abc909350d3", null, "User", "USER" },
                    { "1563f100-3d96-4fb1-b517-8b135d900b61", null, "Admin", "ADMIN" }
                });
        }
    }
}
