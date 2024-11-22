using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class orderandaddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Address_Id",
                table: "orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_orders_Address_Id",
                table: "orders",
                column: "Address_Id",
                unique: true,
                filter: "[Address_Id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_Address_Address_Id",
                table: "orders",
                column: "Address_Id",
                principalTable: "Address",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_Address_Address_Id",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_Address_Id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "Address_Id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Address");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "c955dfc0-360f-4233-87fe-3d7aea023e8c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DESIGNER_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "594961a7-1b30-409f-ba11-3e44fdad4a3f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "EMPLOYEE_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "9a2c65df-cc93-4716-8306-7e8fef92d7f1");
        }
    }
}
