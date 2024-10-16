using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class updateposst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "post_metas");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "product_post",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Feature_image",
                table: "product_post",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image_library",
                table: "product_post",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Short_description",
                table: "product_post",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "product_post");

            migrationBuilder.DropColumn(
                name: "Feature_image",
                table: "product_post");

            migrationBuilder.DropColumn(
                name: "Image_library",
                table: "product_post");

            migrationBuilder.DropColumn(
                name: "Short_description",
                table: "product_post");

            migrationBuilder.CreateTable(
                name: "post_metas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Post_Id = table.Column<long>(type: "bigint", nullable: false),
                    Meta_key = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Meta_value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_metas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_post_metas_product_post_Post_Id",
                        column: x => x.Post_Id,
                        principalTable: "product_post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_post_metas_Post_Id",
                table: "post_metas",
                column: "Post_Id");
        }
    }
}
