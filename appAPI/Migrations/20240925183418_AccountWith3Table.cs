using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class AccountWith3Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "warehouse",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "posts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Activity_history",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_AccountId",
                table: "warehouse",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_posts_AccountId",
                table: "posts",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_history_AccountId",
                table: "Activity_history",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_history_AspNetUsers_AccountId",
                table: "Activity_history",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_AspNetUsers_AccountId",
                table: "posts",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_warehouse_AspNetUsers_AccountId",
                table: "warehouse",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_history_AspNetUsers_AccountId",
                table: "Activity_history");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_AspNetUsers_AccountId",
                table: "posts");

            migrationBuilder.DropForeignKey(
                name: "FK_warehouse_AspNetUsers_AccountId",
                table: "warehouse");

            migrationBuilder.DropIndex(
                name: "IX_warehouse_AccountId",
                table: "warehouse");

            migrationBuilder.DropIndex(
                name: "IX_posts_AccountId",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "IX_Activity_history_AccountId",
                table: "Activity_history");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "warehouse");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Activity_history");
        }
    }
}
