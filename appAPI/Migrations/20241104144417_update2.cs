using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_attributes_product_variants_Product_Variant_Id",
                table: "product_attributes");

            migrationBuilder.DropIndex(
                name: "IX_product_attributes_Product_Variant_Id",
                table: "product_attributes");

            migrationBuilder.DropColumn(
                name: "Stock_quantity",
                table: "product_variants");

            migrationBuilder.AddColumn<long>(
                name: "Product_VariantId",
                table: "product_attributes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_attributes_Product_VariantId",
                table: "product_attributes",
                column: "Product_VariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_product_attributes_product_variants_Product_VariantId",
                table: "product_attributes",
                column: "Product_VariantId",
                principalTable: "product_variants",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_attributes_product_variants_Product_VariantId",
                table: "product_attributes");

            migrationBuilder.DropIndex(
                name: "IX_product_attributes_Product_VariantId",
                table: "product_attributes");

            migrationBuilder.DropColumn(
                name: "Product_VariantId",
                table: "product_attributes");

            migrationBuilder.AddColumn<int>(
                name: "Stock_quantity",
                table: "product_variants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_product_attributes_Product_Variant_Id",
                table: "product_attributes",
                column: "Product_Variant_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_attributes_product_variants_Product_Variant_Id",
                table: "product_attributes",
                column: "Product_Variant_Id",
                principalTable: "product_variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
