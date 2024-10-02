using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class updatedatbase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cartdetails_post_products_Product_id",
                table: "Cartdetails");

            migrationBuilder.DropForeignKey(
                name: "FK_categories_posts_PostsId",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_carts_CartId",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_AspNetUsers_AccountId",
                table: "posts");

            migrationBuilder.DropForeignKey(
                name: "FK_product_attributes_Attributes_Product_Id",
                table: "product_attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_product_attributes_post_products_Attribute_Id",
                table: "product_attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_product_variants_Attributes_Attribute_Id",
                table: "product_variants");

            migrationBuilder.DropForeignKey(
                name: "FK_product_variants_post_products_Product_Id",
                table: "product_variants");

            migrationBuilder.DropForeignKey(
                name: "FK_tags_posts_PostsId",
                table: "tags");

            migrationBuilder.DropForeignKey(
                name: "FK_wishlist_post_products_Product_id",
                table: "wishlist");

            migrationBuilder.DropTable(
                name: "DesignerProducts");

            migrationBuilder.DropTable(
                name: "post_products");

            migrationBuilder.DropIndex(
                name: "IX_wishlist_Product_id",
                table: "wishlist");

            migrationBuilder.DropIndex(
                name: "IX_wishlist_User_id",
                table: "wishlist");

            migrationBuilder.DropIndex(
                name: "users_email_unique",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_tags_PostsId",
                table: "tags");

            migrationBuilder.DropIndex(
                name: "tags_type_index",
                table: "tags");

            migrationBuilder.DropIndex(
                name: "IX_product_variants_attribute_id",
                table: "product_variants");

            migrationBuilder.DropIndex(
                name: "UK_product_variants_sku",
                table: "product_variants");

            migrationBuilder.DropIndex(
                name: "IX_posts_author_id",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "IX_posts_slug",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "IX_posts_status",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "IX_posts_type",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "UK_posts_slug_deleted_at",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "IX_post_metas_meta_key",
                table: "post_metas");

            migrationBuilder.DropIndex(
                name: "IX_orders_CartId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "orders_address_fulltext",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "orders_approved_at_index",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "orders_code_index",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "orders_name_index",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "orders_phone_number_index",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "orders_status_index",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "orders_warehouse_id_index",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "UQ__options__A2E9140E2B971836",
                table: "options");

            migrationBuilder.DropIndex(
                name: "categories_slug_unique",
                table: "categories");

            migrationBuilder.DropIndex(
                name: "categories_type_index",
                table: "categories");

            migrationBuilder.DropIndex(
                name: "IX_categories_PostsId",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "Product_id",
                table: "wishlist");

            migrationBuilder.DropColumn(
                name: "PostsId",
                table: "tags");

            migrationBuilder.DropColumn(
                name: "Attribute_Id",
                table: "product_variants");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "Warehouse_id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "PostsId",
                table: "categories");

            migrationBuilder.RenameColumn(
                name: "Product_Id",
                table: "product_variants",
                newName: "Post_Id");

            migrationBuilder.RenameIndex(
                name: "IX_product_variants_product_id",
                table: "product_variants",
                newName: "IX_product_variants_Post_Id");

            migrationBuilder.RenameColumn(
                name: "Product_Id",
                table: "product_attributes",
                newName: "Product_Variants_Id");

            migrationBuilder.RenameIndex(
                name: "IX_product_attributes_attribute_id",
                table: "product_attributes",
                newName: "IX_product_attributes_Attribute_Id");

            migrationBuilder.RenameIndex(
                name: "IX_product_attributes_product_id",
                table: "product_attributes",
                newName: "IX_product_attributes_Product_Variants_Id");

            migrationBuilder.RenameIndex(
                name: "IX_post_tags_tag_id",
                table: "post_tags",
                newName: "IX_post_tags_Tag_Id");

            migrationBuilder.RenameIndex(
                name: "IX_post_tags_post_id",
                table: "post_tags",
                newName: "IX_post_tags_Post_Id");

            migrationBuilder.RenameIndex(
                name: "IX_post_categories_post_id",
                table: "post_categories",
                newName: "IX_post_categories_Post_Id");

            migrationBuilder.RenameIndex(
                name: "IX_post_categories_category_id",
                table: "post_categories",
                newName: "IX_post_categories_Category_Id");

            migrationBuilder.RenameIndex(
                name: "orders_user_id_index",
                table: "orders",
                newName: "IX_orders_User_id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "order_details",
                newName: "ProductVariants_Id");

            migrationBuilder.AddColumn<long>(
                name: "AttributesId",
                table: "product_variants",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted_at",
                table: "product_variants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Designerid_Designer",
                table: "product_variants",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "posts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "Designer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Product_variants_wishlist",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_variants_id = table.Column<long>(type: "bigint", nullable: false),
                    Wishlist_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_variants_wishlist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_variants_wishlist_product_variants_Product_variants_id",
                        column: x => x.Product_variants_id,
                        principalTable: "product_variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_variants_wishlist_wishlist_Wishlist_id",
                        column: x => x.Wishlist_id,
                        principalTable: "wishlist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_wishlist_User_id",
                table: "wishlist",
                column: "User_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_variants_AttributesId",
                table: "product_variants",
                column: "AttributesId");

            migrationBuilder.CreateIndex(
                name: "IX_product_variants_Designerid_Designer",
                table: "product_variants",
                column: "Designerid_Designer");

            migrationBuilder.CreateIndex(
                name: "IX_order_details_ProductVariants_Id",
                table: "order_details",
                column: "ProductVariants_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_variants_wishlist_Product_variants_id",
                table: "Product_variants_wishlist",
                column: "Product_variants_id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_variants_wishlist_Wishlist_id",
                table: "Product_variants_wishlist",
                column: "Wishlist_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cartdetails_product_variants_Product_id",
                table: "Cartdetails",
                column: "Product_id",
                principalTable: "product_variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_details_product_variants_ProductVariants_Id",
                table: "order_details",
                column: "ProductVariants_Id",
                principalTable: "product_variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_users_User_id",
                table: "orders",
                column: "User_id",
                principalTable: "users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_AspNetUsers_AccountId",
                table: "posts",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_attributes_Attributes_Attribute_Id",
                table: "product_attributes",
                column: "Attribute_Id",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_attributes_product_variants_Product_Variants_Id",
                table: "product_attributes",
                column: "Product_Variants_Id",
                principalTable: "product_variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_variants_Attributes_AttributesId",
                table: "product_variants",
                column: "AttributesId",
                principalTable: "Attributes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_variants_Designer_Designerid_Designer",
                table: "product_variants",
                column: "Designerid_Designer",
                principalTable: "Designer",
                principalColumn: "id_Designer");

            migrationBuilder.AddForeignKey(
                name: "FK_product_variants_posts_Post_Id",
                table: "product_variants",
                column: "Post_Id",
                principalTable: "posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cartdetails_product_variants_Product_id",
                table: "Cartdetails");

            migrationBuilder.DropForeignKey(
                name: "FK_order_details_product_variants_ProductVariants_Id",
                table: "order_details");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_users_User_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_AspNetUsers_AccountId",
                table: "posts");

            migrationBuilder.DropForeignKey(
                name: "FK_product_attributes_Attributes_Attribute_Id",
                table: "product_attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_product_attributes_product_variants_Product_Variants_Id",
                table: "product_attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_product_variants_Attributes_AttributesId",
                table: "product_variants");

            migrationBuilder.DropForeignKey(
                name: "FK_product_variants_Designer_Designerid_Designer",
                table: "product_variants");

            migrationBuilder.DropForeignKey(
                name: "FK_product_variants_posts_Post_Id",
                table: "product_variants");

            migrationBuilder.DropTable(
                name: "Product_variants_wishlist");

            migrationBuilder.DropIndex(
                name: "IX_wishlist_User_id",
                table: "wishlist");

            migrationBuilder.DropIndex(
                name: "IX_product_variants_AttributesId",
                table: "product_variants");

            migrationBuilder.DropIndex(
                name: "IX_product_variants_Designerid_Designer",
                table: "product_variants");

            migrationBuilder.DropIndex(
                name: "IX_order_details_ProductVariants_Id",
                table: "order_details");

            migrationBuilder.DropColumn(
                name: "AttributesId",
                table: "product_variants");

            migrationBuilder.DropColumn(
                name: "Deleted_at",
                table: "product_variants");

            migrationBuilder.DropColumn(
                name: "Designerid_Designer",
                table: "product_variants");

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "Designer");

            migrationBuilder.RenameColumn(
                name: "Post_Id",
                table: "product_variants",
                newName: "Product_Id");

            migrationBuilder.RenameIndex(
                name: "IX_product_variants_Post_Id",
                table: "product_variants",
                newName: "IX_product_variants_product_id");

            migrationBuilder.RenameColumn(
                name: "Product_Variants_Id",
                table: "product_attributes",
                newName: "Product_Id");

            migrationBuilder.RenameIndex(
                name: "IX_product_attributes_Attribute_Id",
                table: "product_attributes",
                newName: "IX_product_attributes_attribute_id");

            migrationBuilder.RenameIndex(
                name: "IX_product_attributes_Product_Variants_Id",
                table: "product_attributes",
                newName: "IX_product_attributes_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_post_tags_Tag_Id",
                table: "post_tags",
                newName: "IX_post_tags_tag_id");

            migrationBuilder.RenameIndex(
                name: "IX_post_tags_Post_Id",
                table: "post_tags",
                newName: "IX_post_tags_post_id");

            migrationBuilder.RenameIndex(
                name: "IX_post_categories_Post_Id",
                table: "post_categories",
                newName: "IX_post_categories_post_id");

            migrationBuilder.RenameIndex(
                name: "IX_post_categories_Category_Id",
                table: "post_categories",
                newName: "IX_post_categories_category_id");

            migrationBuilder.RenameIndex(
                name: "IX_orders_User_id",
                table: "orders",
                newName: "orders_user_id_index");

            migrationBuilder.RenameColumn(
                name: "ProductVariants_Id",
                table: "order_details",
                newName: "ProductId");

            migrationBuilder.AddColumn<long>(
                name: "Product_id",
                table: "wishlist",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PostsId",
                table: "tags",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Attribute_Id",
                table: "product_variants",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "posts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "CartId",
                table: "orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Warehouse_id",
                table: "orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PostsId",
                table: "categories",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "post_products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Post_Id = table.Column<long>(type: "bigint", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Designer_Id = table.Column<long>(type: "bigint", unicode: false, maxLength: 100, nullable: false),
                    Sku = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_post_products_posts_Post_Id",
                        column: x => x.Post_Id,
                        principalTable: "posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesignerProducts",
                columns: table => new
                {
                    Designerid_Designer = table.Column<long>(type: "bigint", nullable: false),
                    Post_productsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignerProducts", x => new { x.Designerid_Designer, x.Post_productsId });
                    table.ForeignKey(
                        name: "FK_DesignerProducts_Designer_Designerid_Designer",
                        column: x => x.Designerid_Designer,
                        principalTable: "Designer",
                        principalColumn: "id_Designer",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesignerProducts_post_products_Post_productsId",
                        column: x => x.Post_productsId,
                        principalTable: "post_products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_wishlist_Product_id",
                table: "wishlist",
                column: "Product_id");

            migrationBuilder.CreateIndex(
                name: "IX_wishlist_User_id",
                table: "wishlist",
                column: "User_id");

            migrationBuilder.CreateIndex(
                name: "users_email_unique",
                table: "users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tags_PostsId",
                table: "tags",
                column: "PostsId");

            migrationBuilder.CreateIndex(
                name: "tags_type_index",
                table: "tags",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_product_variants_attribute_id",
                table: "product_variants",
                column: "Attribute_Id");

            migrationBuilder.CreateIndex(
                name: "UK_product_variants_sku",
                table: "product_variants",
                column: "Sku",
                unique: true,
                filter: "[Sku] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_posts_author_id",
                table: "posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_posts_slug",
                table: "posts",
                column: "Slug");

            migrationBuilder.CreateIndex(
                name: "IX_posts_status",
                table: "posts",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_posts_type",
                table: "posts",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "UK_posts_slug_deleted_at",
                table: "posts",
                columns: new[] { "Slug", "Deleted_at" },
                unique: true,
                filter: "[Slug] IS NOT NULL AND [Deleted_at] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_post_metas_meta_key",
                table: "post_metas",
                column: "Meta_key");

            migrationBuilder.CreateIndex(
                name: "IX_orders_CartId",
                table: "orders",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "orders_address_fulltext",
                table: "orders",
                column: "Address");

            migrationBuilder.CreateIndex(
                name: "orders_approved_at_index",
                table: "orders",
                column: "Approved_at");

            migrationBuilder.CreateIndex(
                name: "orders_code_index",
                table: "orders",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "orders_name_index",
                table: "orders",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "orders_phone_number_index",
                table: "orders",
                column: "Phone_number");

            migrationBuilder.CreateIndex(
                name: "orders_status_index",
                table: "orders",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "orders_warehouse_id_index",
                table: "orders",
                column: "Warehouse_id");

            migrationBuilder.CreateIndex(
                name: "UQ__options__A2E9140E2B971836",
                table: "options",
                column: "Optin_name",
                unique: true,
                filter: "[Optin_name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "categories_slug_unique",
                table: "categories",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "categories_type_index",
                table: "categories",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_categories_PostsId",
                table: "categories",
                column: "PostsId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignerProducts_Post_productsId",
                table: "DesignerProducts",
                column: "Post_productsId");

            migrationBuilder.CreateIndex(
                name: "IX_post_products_post_id",
                table: "post_products",
                column: "Post_Id");

            migrationBuilder.CreateIndex(
                name: "UK_post_products_sku",
                table: "post_products",
                column: "Sku",
                unique: true,
                filter: "[Sku] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Cartdetails_post_products_Product_id",
                table: "Cartdetails",
                column: "Product_id",
                principalTable: "post_products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_categories_posts_PostsId",
                table: "categories",
                column: "PostsId",
                principalTable: "posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_carts_CartId",
                table: "orders",
                column: "CartId",
                principalTable: "carts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_AspNetUsers_AccountId",
                table: "posts",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_attributes_Attributes_Product_Id",
                table: "product_attributes",
                column: "Product_Id",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_attributes_post_products_Attribute_Id",
                table: "product_attributes",
                column: "Attribute_Id",
                principalTable: "post_products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_variants_Attributes_Attribute_Id",
                table: "product_variants",
                column: "Attribute_Id",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_variants_post_products_Product_Id",
                table: "product_variants",
                column: "Product_Id",
                principalTable: "post_products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tags_posts_PostsId",
                table: "tags",
                column: "PostsId",
                principalTable: "posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_wishlist_post_products_Product_id",
                table: "wishlist",
                column: "Product_id",
                principalTable: "post_products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
