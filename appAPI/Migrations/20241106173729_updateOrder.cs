using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class updateOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_users_UserId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_order_details_product_attributes_ProductVariants_Id",
                table: "order_details");

            migrationBuilder.DropIndex(
                name: "IX_order_details_ProductVariants_Id",
                table: "order_details");

            migrationBuilder.DropIndex(
                name: "IX_Address_UserId",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "Phone_number",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "Created_at",
                table: "order_details");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "order_details");

            migrationBuilder.DropColumn(
                name: "ProductTitle",
                table: "order_details");

            migrationBuilder.DropColumn(
                name: "ProductVariants_Id",
                table: "order_details");

            migrationBuilder.DropColumn(
                name: "RegularPrice",
                table: "order_details");

            migrationBuilder.DropColumn(
                name: "RemainingStockQuantity",
                table: "order_details");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "order_details");

            migrationBuilder.DropColumn(
                name: "Update_at",
                table: "order_details");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Address");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByAdminId",
                table: "orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_CreatedByAdminId",
                table: "orders",
                column: "CreatedByAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_order_details_Product_Attribute_Id",
                table: "order_details",
                column: "Product_Attribute_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Address_User_Id",
                table: "Address",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_users_User_Id",
                table: "Address",
                column: "User_Id",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_details_product_attributes_Product_Attribute_Id",
                table: "order_details",
                column: "Product_Attribute_Id",
                principalTable: "product_attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_AspNetUsers_CreatedByAdminId",
                table: "orders",
                column: "CreatedByAdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_users_User_Id",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_order_details_product_attributes_Product_Attribute_Id",
                table: "order_details");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_AspNetUsers_CreatedByAdminId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_CreatedByAdminId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_order_details_Product_Attribute_Id",
                table: "order_details");

            migrationBuilder.DropIndex(
                name: "IX_Address_User_Id",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "CreatedByAdminId",
                table: "orders");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "orders",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "orders",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "orders",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone_number",
                table: "orders",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_at",
                table: "order_details",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "order_details",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ProductTitle",
                table: "order_details",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductVariants_Id",
                table: "order_details",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RegularPrice",
                table: "order_details",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "RemainingStockQuantity",
                table: "order_details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                table: "order_details",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "Update_at",
                table: "order_details",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Address",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_details_ProductVariants_Id",
                table: "order_details",
                column: "ProductVariants_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserId",
                table: "Address",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_users_UserId",
                table: "Address",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_details_product_attributes_ProductVariants_Id",
                table: "order_details",
                column: "ProductVariants_Id",
                principalTable: "product_attributes",
                principalColumn: "Id");
        }
    }
}
