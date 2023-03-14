using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.PaymentInvite.Data.Migrations
{
    public partial class DealName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TransactionItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DealItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "TransactionItems");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "DealItems");
        }
    }
}
