using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_attributes_Color_Color_Id",
                table: "product_attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_product_attributes_product_variants_Product_VariantId",
                table: "product_attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_product_attributes_Sizes_Size_Id",
                table: "product_attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_product_variants_Materials_Material_id",
                table: "product_variants");

            migrationBuilder.DropForeignKey(
                name: "FK_product_variants_Styles_Style_id",
                table: "product_variants");

            migrationBuilder.DropForeignKey(
                name: "FK_product_variants_Textile_Technologies_Textile_technology_id",
                table: "product_variants");

            migrationBuilder.DropIndex(
                name: "IX_product_attributes_Product_VariantId",
                table: "product_attributes");

            migrationBuilder.DropColumn(
                name: "Product_VariantId",
                table: "product_attributes");

            migrationBuilder.AlterColumn<long>(
                name: "Textile_technology_id",
                table: "product_variants",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "Style_id",
                table: "product_variants",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "Material_id",
                table: "product_variants",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "Size_Id",
                table: "product_attributes",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "Color_Id",
                table: "product_attributes",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_product_attributes_Product_Variant_Id",
                table: "product_attributes",
                column: "Product_Variant_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_attributes_Color_Color_Id",
                table: "product_attributes",
                column: "Color_Id",
                principalTable: "Color",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_attributes_product_variants_Product_Variant_Id",
                table: "product_attributes",
                column: "Product_Variant_Id",
                principalTable: "product_variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_attributes_Sizes_Size_Id",
                table: "product_attributes",
                column: "Size_Id",
                principalTable: "Sizes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_variants_Materials_Material_id",
                table: "product_variants",
                column: "Material_id",
                principalTable: "Materials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_variants_Styles_Style_id",
                table: "product_variants",
                column: "Style_id",
                principalTable: "Styles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_variants_Textile_Technologies_Textile_technology_id",
                table: "product_variants",
                column: "Textile_technology_id",
                principalTable: "Textile_Technologies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_attributes_Color_Color_Id",
                table: "product_attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_product_attributes_product_variants_Product_Variant_Id",
                table: "product_attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_product_attributes_Sizes_Size_Id",
                table: "product_attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_product_variants_Materials_Material_id",
                table: "product_variants");

            migrationBuilder.DropForeignKey(
                name: "FK_product_variants_Styles_Style_id",
                table: "product_variants");

            migrationBuilder.DropForeignKey(
                name: "FK_product_variants_Textile_Technologies_Textile_technology_id",
                table: "product_variants");

            migrationBuilder.DropIndex(
                name: "IX_product_attributes_Product_Variant_Id",
                table: "product_attributes");

            migrationBuilder.AlterColumn<long>(
                name: "Textile_technology_id",
                table: "product_variants",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Style_id",
                table: "product_variants",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Material_id",
                table: "product_variants",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Size_Id",
                table: "product_attributes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Color_Id",
                table: "product_attributes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

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
                name: "FK_product_attributes_Color_Color_Id",
                table: "product_attributes",
                column: "Color_Id",
                principalTable: "Color",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_attributes_product_variants_Product_VariantId",
                table: "product_attributes",
                column: "Product_VariantId",
                principalTable: "product_variants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_attributes_Sizes_Size_Id",
                table: "product_attributes",
                column: "Size_Id",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_variants_Materials_Material_id",
                table: "product_variants",
                column: "Material_id",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_variants_Styles_Style_id",
                table: "product_variants",
                column: "Style_id",
                principalTable: "Styles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_variants_Textile_Technologies_Textile_technology_id",
                table: "product_variants",
                column: "Textile_technology_id",
                principalTable: "Textile_Technologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
