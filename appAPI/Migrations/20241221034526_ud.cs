using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appAPI.Migrations
{
    public partial class ud : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotlaVoucher",
                table: "orders");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalVoucher",
                table: "orders",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalVoucher",
                table: "orders");

            migrationBuilder.AddColumn<decimal>(
                name: "TotlaVoucher",
                table: "orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
