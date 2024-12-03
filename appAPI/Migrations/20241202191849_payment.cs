using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class payment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "baivietnoibat",
                table: "product_post");

            migrationBuilder.DropColumn(
                name: "bannernoibat",
                table: "product_post");

            migrationBuilder.DropColumn(
                name: "duannoibat",
                table: "product_post");

            migrationBuilder.DropColumn(
                name: "quytrinh",
                table: "product_post");

            migrationBuilder.DropColumn(
                name: "trendy",
                table: "product_post");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "50eb828b-e5ba-4792-8ff0-1de168f4753a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DESIGNER_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "69f1322e-225f-406f-8f31-0106a9839d0e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "EMPLOYEE_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "2cf87cb7-6332-45c1-bbcb-b69e48dd8f71");

            migrationBuilder.InsertData(
                table: "Payment",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1L, "Thanh toán bằng tiền mặt", "Tiền mặt" },
                    { 2L, "Thanh toán qua chuyển khoản ngân hàng", "Chuyển khoản" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.AddColumn<bool>(
                name: "baivietnoibat",
                table: "product_post",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "bannernoibat",
                table: "product_post",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "duannoibat",
                table: "product_post",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "quytrinh",
                table: "product_post",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "trendy",
                table: "product_post",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "8978ae3c-6289-4de5-a4a4-02b76918cce4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DESIGNER_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "7b24352e-bcc3-45ff-89c8-f079ac70580b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "EMPLOYEE_ROLE_ID",
                column: "ConcurrencyStamp",
                value: "52f108d2-80ef-4861-9440-b4e46c0b0760");
        }
    }
}
