using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class updatedatabasev5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_users_UserId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_posts_Post_id",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_post_categories_posts_Post_Id",
                table: "post_categories");

            migrationBuilder.DropForeignKey(
                name: "FK_post_metas_posts_Post_Id",
                table: "post_metas");

            migrationBuilder.DropForeignKey(
                name: "FK_post_tags_posts_Post_Id",
                table: "post_tags");

            migrationBuilder.DropForeignKey(
                name: "FK_product_variants_posts_Post_Id",
                table: "product_variants");

            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropTable(
                name: "warehouse");

            migrationBuilder.AddColumn<long>(
                name: "Color_id",
                table: "product_variants",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Material_id",
                table: "product_variants",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Size_id",
                table: "product_variants",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Style_id",
                table: "product_variants",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Textile_technology_id",
                table: "product_variants",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Ward_commune",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Address",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Steet",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Province_city",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "District",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delete_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delete_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delete_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Styles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delete_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Styles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Textile_Technologies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delete_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Textile_Technologies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "product_post",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Slug = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Post_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created_by = table.Column<long>(type: "bigint", nullable: false),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false),
                    Deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ColorId = table.Column<long>(type: "bigint", nullable: true),
                    MaterialId = table.Column<long>(type: "bigint", nullable: true),
                    SizeId = table.Column<long>(type: "bigint", nullable: true),
                    StyleId = table.Column<long>(type: "bigint", nullable: true),
                    Textile_technologyId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_post_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_product_post_Color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Color",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_product_post_Designer_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Designer",
                        principalColumn: "id_Designer",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_post_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_product_post_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_product_post_Styles_StyleId",
                        column: x => x.StyleId,
                        principalTable: "Styles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_product_post_Textile_Technologies_Textile_technologyId",
                        column: x => x.Textile_technologyId,
                        principalTable: "Textile_Technologies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_product_variants_Color_id",
                table: "product_variants",
                column: "Color_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_variants_Material_id",
                table: "product_variants",
                column: "Material_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_variants_Size_id",
                table: "product_variants",
                column: "Size_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_variants_Style_id",
                table: "product_variants",
                column: "Style_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_variants_Textile_technology_id",
                table: "product_variants",
                column: "Textile_technology_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_post_AccountId",
                table: "product_post",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_product_post_AuthorId",
                table: "product_post",
                column: "AuthorId");

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
                name: "FK_Address_users_UserId",
                table: "Address",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_product_post_Post_id",
                table: "Comments",
                column: "Post_id",
                principalTable: "product_post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_post_categories_product_post_Post_Id",
                table: "post_categories",
                column: "Post_Id",
                principalTable: "product_post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_post_metas_product_post_Post_Id",
                table: "post_metas",
                column: "Post_Id",
                principalTable: "product_post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_post_tags_product_post_Post_Id",
                table: "post_tags",
                column: "Post_Id",
                principalTable: "product_post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_variants_Color_Color_id",
                table: "product_variants",
                column: "Color_id",
                principalTable: "Color",
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
                name: "FK_product_variants_product_post_Post_Id",
                table: "product_variants",
                column: "Post_Id",
                principalTable: "product_post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_variants_Sizes_Size_id",
                table: "product_variants",
                column: "Size_id",
                principalTable: "Sizes",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_users_UserId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_product_post_Post_id",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_post_categories_product_post_Post_Id",
                table: "post_categories");

            migrationBuilder.DropForeignKey(
                name: "FK_post_metas_product_post_Post_Id",
                table: "post_metas");

            migrationBuilder.DropForeignKey(
                name: "FK_post_tags_product_post_Post_Id",
                table: "post_tags");

            migrationBuilder.DropForeignKey(
                name: "FK_product_variants_Color_Color_id",
                table: "product_variants");

            migrationBuilder.DropForeignKey(
                name: "FK_product_variants_Materials_Material_id",
                table: "product_variants");

            migrationBuilder.DropForeignKey(
                name: "FK_product_variants_product_post_Post_Id",
                table: "product_variants");

            migrationBuilder.DropForeignKey(
                name: "FK_product_variants_Sizes_Size_id",
                table: "product_variants");

            migrationBuilder.DropForeignKey(
                name: "FK_product_variants_Styles_Style_id",
                table: "product_variants");

            migrationBuilder.DropForeignKey(
                name: "FK_product_variants_Textile_Technologies_Textile_technology_id",
                table: "product_variants");

            migrationBuilder.DropTable(
                name: "product_post");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "Styles");

            migrationBuilder.DropTable(
                name: "Textile_Technologies");

            migrationBuilder.DropIndex(
                name: "IX_product_variants_Color_id",
                table: "product_variants");

            migrationBuilder.DropIndex(
                name: "IX_product_variants_Material_id",
                table: "product_variants");

            migrationBuilder.DropIndex(
                name: "IX_product_variants_Size_id",
                table: "product_variants");

            migrationBuilder.DropIndex(
                name: "IX_product_variants_Style_id",
                table: "product_variants");

            migrationBuilder.DropIndex(
                name: "IX_product_variants_Textile_technology_id",
                table: "product_variants");

            migrationBuilder.DropColumn(
                name: "Color_id",
                table: "product_variants");

            migrationBuilder.DropColumn(
                name: "Material_id",
                table: "product_variants");

            migrationBuilder.DropColumn(
                name: "Size_id",
                table: "product_variants");

            migrationBuilder.DropColumn(
                name: "Style_id",
                table: "product_variants");

            migrationBuilder.DropColumn(
                name: "Textile_technology_id",
                table: "product_variants");

            migrationBuilder.AlterColumn<string>(
                name: "Ward_commune",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Address",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Steet",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Province_city",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "District",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created_by = table.Column<long>(type: "bigint", nullable: false),
                    Deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Post_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Slug = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_posts_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "warehouse",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warehouse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_warehouse_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_posts_AccountId",
                table: "posts",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_AccountId",
                table: "warehouse",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_users_UserId",
                table: "Address",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_posts_Post_id",
                table: "Comments",
                column: "Post_id",
                principalTable: "posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_post_categories_posts_Post_Id",
                table: "post_categories",
                column: "Post_Id",
                principalTable: "posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_post_metas_posts_Post_Id",
                table: "post_metas",
                column: "Post_Id",
                principalTable: "posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_post_tags_posts_Post_Id",
                table: "post_tags",
                column: "Post_Id",
                principalTable: "posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_variants_posts_Post_Id",
                table: "product_variants",
                column: "Post_Id",
                principalTable: "posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
