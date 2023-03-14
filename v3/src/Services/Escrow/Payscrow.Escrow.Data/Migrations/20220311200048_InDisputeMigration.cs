using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.Escrow.Data.Migrations
{
    public partial class InDisputeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Settlements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EscrowCode",
                table: "EscrowTransactions",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InDispute",
                table: "EscrowTransactions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Settlements");

            migrationBuilder.DropColumn(
                name: "EscrowCode",
                table: "EscrowTransactions");

            migrationBuilder.DropColumn(
                name: "InDispute",
                table: "EscrowTransactions");
        }
    }
}
