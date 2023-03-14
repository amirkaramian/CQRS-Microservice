using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.PaymentInvite.Data.Migrations
{
    public partial class ContactAddressMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "TransactionItems",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TransactionItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "TransactionItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactAddress_City",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactAddress_Country",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactAddress_State",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactAddress_Street",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactAddress_ZipCode",
                table: "Person",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "TransactionItems");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "TransactionItems");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "TransactionItems");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "ContactAddress_City",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "ContactAddress_Country",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "ContactAddress_State",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "ContactAddress_Street",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "ContactAddress_ZipCode",
                table: "Person");
        }
    }
}
