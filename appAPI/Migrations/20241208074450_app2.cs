using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class app2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products_returned",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDetailId = table.Column<long>(type: "bigint", nullable: false),
                    ReturnReason = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created_by = table.Column<long>(type: "bigint", nullable: true),
                    Update_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products_returned", x => x.Id);
                    table.ForeignKey(
                        name: "FK_products_returned_order_details_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "order_details",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products_returned");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "68279c94-1fb9-4b0d-b040-1852002ce38f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DESIGNER_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "96aaa65c-be64-4246-ba0c-e813aefa2a7a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "EMPLOYEE_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "d8cd820f-933a-42f9-a401-300c1d792d31");
        }
    }
}
