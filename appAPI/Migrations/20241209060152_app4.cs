using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class app4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_orders_Address_Id",
                table: "orders");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "cecbd315-cf66-4448-a75d-e6b4735569b4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DESIGNER_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "a4210228-05e9-4f07-bb35-3c6ae0055372");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "EMPLOYEE_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "1fb1fdd6-a258-4a43-9dbc-846fee364a94");

            migrationBuilder.CreateIndex(
                name: "IX_orders_Address_Id",
                table: "orders",
                column: "Address_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_orders_Address_Id",
                table: "orders");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "a6a20e27-5063-4002-b143-f7e93a6d9c5c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DESIGNER_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "61fe8e22-4582-4f03-9964-e00195e34198");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "EMPLOYEE_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "b53d398a-7bc0-43a2-8312-ee098ead2a50");

            migrationBuilder.CreateIndex(
                name: "IX_orders_Address_Id",
                table: "orders",
                column: "Address_Id",
                unique: true,
                filter: "[Address_Id] IS NOT NULL");
        }
    }
}
