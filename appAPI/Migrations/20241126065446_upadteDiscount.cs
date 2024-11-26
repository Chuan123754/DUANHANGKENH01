using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class upadteDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_p_Variants_Discounts_Discount_Discount_Id",
                table: "p_Variants_Discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_p_Variants_Discounts_product_attributes_P_attribute_Id",
                table: "p_Variants_Discounts");

            migrationBuilder.DropColumn(
                name: "New_price",
                table: "p_Variants_Discounts");

            migrationBuilder.DropColumn(
                name: "Old_price",
                table: "p_Variants_Discounts");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "p_Variants_Discounts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "P_attribute_Id",
                table: "p_Variants_Discounts",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "Discount_Id",
                table: "p_Variants_Discounts",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<DateTime>(
                name: "AppliedDate",
                table: "p_Variants_Discounts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Updated_by",
                table: "Discount",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Update_at",
                table: "Discount",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Type_of_promotion",
                table: "Discount",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Discount",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Discount",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "Created_by",
                table: "Discount",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Create_at",
                table: "Discount",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Discount",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount_value",
                table: "Discount",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsGlobal",
                table: "Discount",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "8fecc611-39c1-4616-a740-6a97fdf452d9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DESIGNER_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "85866c64-86dc-44aa-959b-846989dd9ba6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "EMPLOYEE_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "37377c00-8ba8-4b93-a5f0-1d08a7e2d9a1");

            migrationBuilder.AddForeignKey(
                name: "FK_p_Variants_Discounts_Discount_Discount_Id",
                table: "p_Variants_Discounts",
                column: "Discount_Id",
                principalTable: "Discount",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_p_Variants_Discounts_product_attributes_P_attribute_Id",
                table: "p_Variants_Discounts",
                column: "P_attribute_Id",
                principalTable: "product_attributes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_p_Variants_Discounts_Discount_Discount_Id",
                table: "p_Variants_Discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_p_Variants_Discounts_product_attributes_P_attribute_Id",
                table: "p_Variants_Discounts");

            migrationBuilder.DropColumn(
                name: "AppliedDate",
                table: "p_Variants_Discounts");

            migrationBuilder.DropColumn(
                name: "Discount_value",
                table: "Discount");

            migrationBuilder.DropColumn(
                name: "IsGlobal",
                table: "Discount");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "p_Variants_Discounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "P_attribute_Id",
                table: "p_Variants_Discounts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Discount_Id",
                table: "p_Variants_Discounts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "New_price",
                table: "p_Variants_Discounts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Old_price",
                table: "p_Variants_Discounts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<long>(
                name: "Updated_by",
                table: "Discount",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Update_at",
                table: "Discount",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type_of_promotion",
                table: "Discount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Discount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Discount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Created_by",
                table: "Discount",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Create_at",
                table: "Discount",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Discount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_p_Variants_Discounts_Discount_Discount_Id",
                table: "p_Variants_Discounts",
                column: "Discount_Id",
                principalTable: "Discount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_p_Variants_Discounts_product_attributes_P_attribute_Id",
                table: "p_Variants_Discounts",
                column: "P_attribute_Id",
                principalTable: "product_attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
