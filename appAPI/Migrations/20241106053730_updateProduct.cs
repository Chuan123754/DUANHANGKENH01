using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class updateProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Regular_price",
                table: "product_variants");

            migrationBuilder.DropColumn(
                name: "Sale_price",
                table: "product_variants");

            migrationBuilder.DropColumn(
                name: "Sku",
                table: "product_variants");

            migrationBuilder.RenameColumn(
                name: "Sku",
                table: "product_attributes",
                newName: "SKU");

            migrationBuilder.AlterColumn<string>(
                name: "SKU",
                table: "product_attributes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<long>(
                name: "Regular_price",
                table: "product_attributes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Sale_price",
                table: "product_attributes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "product_attributes",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Regular_price",
                table: "product_attributes");

            migrationBuilder.DropColumn(
                name: "Sale_price",
                table: "product_attributes");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "product_attributes");

            migrationBuilder.RenameColumn(
                name: "SKU",
                table: "product_attributes",
                newName: "Sku");

            migrationBuilder.AddColumn<long>(
                name: "Regular_price",
                table: "product_variants",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Sale_price",
                table: "product_variants",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sku",
                table: "product_variants",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Sku",
                table: "product_attributes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
