using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.PaymentInvite.Data.Migrations
{
    public partial class EscrowChargeFee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BuyerChargeAmount",
                table: "Invites",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SellerChargeAmount",
                table: "Invites",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Invites",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyerChargeAmount",
                table: "Invites");

            migrationBuilder.DropColumn(
                name: "SellerChargeAmount",
                table: "Invites");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Invites");
        }
    }
}
