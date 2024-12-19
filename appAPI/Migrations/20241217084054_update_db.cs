using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class update_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "96975e3d-b39a-4054-9f43-b9ef4177f264");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DESIGNER_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "ded4899a-50e5-40fe-a65d-ef44c1429ac5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "EMPLOYEE_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "5b31b6a6-cfa2-4b23-9dcf-5bfd6b1b8f63");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "ef5fd227-416c-4d2c-b94f-9679491b2d45");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DESIGNER_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "f9261e0b-62a9-4d6d-b6cb-29a564116178");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "EMPLOYEE_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "d388ac27-37ac-4bac-bf93-17c060cec6e0");
        }
    }
}
