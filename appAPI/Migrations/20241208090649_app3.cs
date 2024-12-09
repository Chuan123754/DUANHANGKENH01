using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class app3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_products_returned_OrderDetailId",
                table: "products_returned");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "products_returned",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_products_returned_OrderDetailId",
                table: "products_returned",
                column: "OrderDetailId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_products_returned_OrderDetailId",
                table: "products_returned");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "products_returned");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "28842d89-e2b2-444d-9ec6-872d7754b7ef");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DESIGNER_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "16c38269-fe17-4799-9190-5d8f44527e92");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "EMPLOYEE_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "2a04ff59-f58f-4b09-acde-42c75c8df2ef");

            migrationBuilder.CreateIndex(
                name: "IX_products_returned_OrderDetailId",
                table: "products_returned",
                column: "OrderDetailId");
        }
    }
}
