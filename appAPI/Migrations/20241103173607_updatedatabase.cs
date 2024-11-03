using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class updatedatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_post_Color_ColorId",
                table: "product_post");

            migrationBuilder.DropForeignKey(
                name: "FK_product_post_Materials_MaterialId",
                table: "product_post");

            migrationBuilder.DropForeignKey(
                name: "FK_product_post_Sizes_SizeId",
                table: "product_post");

            migrationBuilder.DropForeignKey(
                name: "FK_product_post_Styles_StyleId",
                table: "product_post");

            migrationBuilder.DropForeignKey(
                name: "FK_product_post_Textile_Technologies_Textile_technologyId",
                table: "product_post");

            migrationBuilder.DropIndex(
                name: "IX_product_post_ColorId",
                table: "product_post");

            migrationBuilder.DropIndex(
                name: "IX_product_post_MaterialId",
                table: "product_post");

            migrationBuilder.DropIndex(
                name: "IX_product_post_SizeId",
                table: "product_post");

            migrationBuilder.DropIndex(
                name: "IX_product_post_StyleId",
                table: "product_post");

            migrationBuilder.DropIndex(
                name: "IX_product_post_Textile_technologyId",
                table: "product_post");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "product_post");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "product_post");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "product_post");

            migrationBuilder.DropColumn(
                name: "StyleId",
                table: "product_post");

            migrationBuilder.DropColumn(
                name: "Textile_technologyId",
                table: "product_post");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ColorId",
                table: "product_post",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MaterialId",
                table: "product_post",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SizeId",
                table: "product_post",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StyleId",
                table: "product_post",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Textile_technologyId",
                table: "product_post",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_post_ColorId",
                table: "product_post",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_product_post_MaterialId",
                table: "product_post",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_product_post_SizeId",
                table: "product_post",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_product_post_StyleId",
                table: "product_post",
                column: "StyleId");

            migrationBuilder.CreateIndex(
                name: "IX_product_post_Textile_technologyId",
                table: "product_post",
                column: "Textile_technologyId");

            migrationBuilder.AddForeignKey(
                name: "FK_product_post_Color_ColorId",
                table: "product_post",
                column: "ColorId",
                principalTable: "Color",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_post_Materials_MaterialId",
                table: "product_post",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_post_Sizes_SizeId",
                table: "product_post",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_post_Styles_StyleId",
                table: "product_post",
                column: "StyleId",
                principalTable: "Styles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_post_Textile_Technologies_Textile_technologyId",
                table: "product_post",
                column: "Textile_technologyId",
                principalTable: "Textile_Technologies",
                principalColumn: "Id");
        }
    }
}
