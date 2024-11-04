using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_variants_Color_ColorId",
                table: "product_variants");

            migrationBuilder.DropForeignKey(
                name: "FK_product_variants_Sizes_SizeId",
                table: "product_variants");

            migrationBuilder.DropIndex(
                name: "IX_product_variants_ColorId",
                table: "product_variants");

            migrationBuilder.DropIndex(
                name: "IX_product_variants_SizeId",
                table: "product_variants");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "product_variants");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "product_variants");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ColorId",
                table: "product_variants",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SizeId",
                table: "product_variants",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_variants_ColorId",
                table: "product_variants",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_product_variants_SizeId",
                table: "product_variants",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_product_variants_Color_ColorId",
                table: "product_variants",
                column: "ColorId",
                principalTable: "Color",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_variants_Sizes_SizeId",
                table: "product_variants",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id");
        }
    }
}
