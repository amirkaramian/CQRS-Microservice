using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.PaymentInvite.Data.Migrations
{
    public partial class TransactionNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Number",
                table: "Transactions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress_City",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress_Country",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress_State",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress_Street",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress_ZipCode",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "TransactionItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Number",
                table: "Transactions",
                column: "Number");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transactions_Number",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress_City",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress_Country",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress_State",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress_Street",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress_ZipCode",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "TransactionItems");
        }
    }
}
