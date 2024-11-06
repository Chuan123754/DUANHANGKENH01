using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class updateDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_p_Variants_Discounts_product_variants_P_variants_Id",
                table: "p_Variants_Discounts");

            migrationBuilder.RenameColumn(
                name: "P_variants_Id",
                table: "p_Variants_Discounts",
                newName: "P_attribute_Id");

            migrationBuilder.RenameIndex(
                name: "IX_p_Variants_Discounts_P_variants_Id",
                table: "p_Variants_Discounts",
                newName: "IX_p_Variants_Discounts_P_attribute_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_p_Variants_Discounts_product_attributes_P_attribute_Id",
                table: "p_Variants_Discounts",
                column: "P_attribute_Id",
                principalTable: "product_attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_p_Variants_Discounts_product_attributes_P_attribute_Id",
                table: "p_Variants_Discounts");

            migrationBuilder.RenameColumn(
                name: "P_attribute_Id",
                table: "p_Variants_Discounts",
                newName: "P_variants_Id");

            migrationBuilder.RenameIndex(
                name: "IX_p_Variants_Discounts_P_attribute_Id",
                table: "p_Variants_Discounts",
                newName: "IX_p_Variants_Discounts_P_variants_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_p_Variants_Discounts_product_variants_P_variants_Id",
                table: "p_Variants_Discounts",
                column: "P_variants_Id",
                principalTable: "product_variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
