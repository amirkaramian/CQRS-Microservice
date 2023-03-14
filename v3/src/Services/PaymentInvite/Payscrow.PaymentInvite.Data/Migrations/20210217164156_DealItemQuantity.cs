using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.PaymentInvite.Data.Migrations
{
    public partial class DealItemQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableQuantity",
                table: "DealItems");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "DealItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "DealItems");

            migrationBuilder.AddColumn<int>(
                name: "AvailableQuantity",
                table: "DealItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
