using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class upodateOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FeeShipping",
                table: "orders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrincipal",
                table: "orders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Totalmoney",
                table: "orders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "8f7f9aa5-5015-4199-a8bb-d8d751e19b4d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DESIGNER_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "44a20122-26d4-4ab1-a260-2ac829bfdbcd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "EMPLOYEE_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "6713832d-f467-4db6-9a5b-870e0b08c88c");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeeShipping",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "TotalPrincipal",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "Totalmoney",
                table: "orders");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "d7080d57-77d6-4d9a-8a79-dc1baa0580c6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DESIGNER_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "d3bbe0c7-a261-4a76-bde1-6f65cbecf165");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "EMPLOYEE_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "11f6ff1d-7ef6-4553-9374-96715178c07d");
        }
    }
}
