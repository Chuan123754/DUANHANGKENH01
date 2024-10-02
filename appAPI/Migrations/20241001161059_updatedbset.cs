using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class updatedbset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_variants_wishlist_product_variants_Product_variants_id",
                table: "Product_variants_wishlist");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_variants_wishlist_wishlist_Wishlist_id",
                table: "Product_variants_wishlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product_variants_wishlist",
                table: "Product_variants_wishlist");

            migrationBuilder.RenameTable(
                name: "Product_variants_wishlist",
                newName: "Product_Variants_Wishlists");

            migrationBuilder.RenameIndex(
                name: "IX_Product_variants_wishlist_Wishlist_id",
                table: "Product_Variants_Wishlists",
                newName: "IX_Product_Variants_Wishlists_Wishlist_id");

            migrationBuilder.RenameIndex(
                name: "IX_Product_variants_wishlist_Product_variants_id",
                table: "Product_Variants_Wishlists",
                newName: "IX_Product_Variants_Wishlists_Product_variants_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product_Variants_Wishlists",
                table: "Product_Variants_Wishlists",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Variants_Wishlists_product_variants_Product_variants_id",
                table: "Product_Variants_Wishlists",
                column: "Product_variants_id",
                principalTable: "product_variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Variants_Wishlists_wishlist_Wishlist_id",
                table: "Product_Variants_Wishlists",
                column: "Wishlist_id",
                principalTable: "wishlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Variants_Wishlists_product_variants_Product_variants_id",
                table: "Product_Variants_Wishlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Variants_Wishlists_wishlist_Wishlist_id",
                table: "Product_Variants_Wishlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product_Variants_Wishlists",
                table: "Product_Variants_Wishlists");

            migrationBuilder.RenameTable(
                name: "Product_Variants_Wishlists",
                newName: "Product_variants_wishlist");

            migrationBuilder.RenameIndex(
                name: "IX_Product_Variants_Wishlists_Wishlist_id",
                table: "Product_variants_wishlist",
                newName: "IX_Product_variants_wishlist_Wishlist_id");

            migrationBuilder.RenameIndex(
                name: "IX_Product_Variants_Wishlists_Product_variants_id",
                table: "Product_variants_wishlist",
                newName: "IX_Product_variants_wishlist_Product_variants_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product_variants_wishlist",
                table: "Product_variants_wishlist",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_variants_wishlist_product_variants_Product_variants_id",
                table: "Product_variants_wishlist",
                column: "Product_variants_id",
                principalTable: "product_variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_variants_wishlist_wishlist_Wishlist_id",
                table: "Product_variants_wishlist",
                column: "Wishlist_id",
                principalTable: "wishlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
