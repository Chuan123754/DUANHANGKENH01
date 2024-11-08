using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class addSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RememberToken",
                table: "users",
                type: "nvarchar(max)",
                maxLength: 2147483647,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(155)",
                oldMaxLength: 155,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ADMIN_ROLE_ID", "43ace886-cc2a-4648-a96b-f40eee2f8d70", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "DESIGNER_ROLE_ID", "a480cfbd-beb0-472e-9e7c-63f1c94f0485", "Designer", "DESIGNER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "EMPLOYEE_ROLE_ID", "35233e38-3bbc-42bd-b7ea-7970cb2fa218", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN_ROLE_ID");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DESIGNER_ROLE_ID");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "EMPLOYEE_ROLE_ID");

            migrationBuilder.AlterColumn<string>(
                name: "RememberToken",
                table: "users",
                type: "nvarchar(155)",
                maxLength: 155,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 2147483647,
                oldNullable: true);
        }
    }
}
