using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class dh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Updated_at",
                table: "tags");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "product_post",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "product_post",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Deleted",
                table: "product_post",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "1755471a-51b5-4675-ad20-02afa9b52bcc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DESIGNER_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "65691ffc-ba41-4e70-8c2c-84ab2201e767");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "EMPLOYEE_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "880e8479-a82c-47e6-a39d-eb1588d01957");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_at",
                table: "tags",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "product_post",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "product_post",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<bool>(
                name: "Deleted",
                table: "product_post",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "a7bebbc7-faed-47f1-bf58-8b68ae00dcc5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DESIGNER_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "d1d94c9a-d79f-4aa4-ace2-cbbaddf091c8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "EMPLOYEE_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "13ff7248-0a38-4649-8667-fcecc5c8259e");
        }
    }
}
