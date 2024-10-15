using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class updatebanner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "product_video",
                table: "product_post",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductPostId",
                table: "banners",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_banners_ProductPostId",
                table: "banners",
                column: "ProductPostId",
                unique: true,
                filter: "[ProductPostId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_banners_product_post_ProductPostId",
                table: "banners",
                column: "ProductPostId",
                principalTable: "product_post",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_banners_product_post_ProductPostId",
                table: "banners");

            migrationBuilder.DropIndex(
                name: "IX_banners_ProductPostId",
                table: "banners");

            migrationBuilder.DropColumn(
                name: "product_video",
                table: "product_post");

            migrationBuilder.DropColumn(
                name: "ProductPostId",
                table: "banners");
        }
    }
}
