using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class updatemoiquanhe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_variants_Designer_Designerid_Designer",
                table: "product_variants");

            migrationBuilder.DropIndex(
                name: "IX_product_variants_Designerid_Designer",
                table: "product_variants");

            migrationBuilder.DropColumn(
                name: "Designerid_Designer",
                table: "product_variants");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Designerid_Designer",
                table: "product_variants",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_variants_Designerid_Designer",
                table: "product_variants",
                column: "Designerid_Designer");

            migrationBuilder.AddForeignKey(
                name: "FK_product_variants_Designer_Designerid_Designer",
                table: "product_variants",
                column: "Designerid_Designer",
                principalTable: "Designer",
                principalColumn: "id_Designer");
        }
    }
}
