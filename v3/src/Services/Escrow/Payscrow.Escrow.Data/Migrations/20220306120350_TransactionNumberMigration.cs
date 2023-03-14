using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.Escrow.Data.Migrations
{
    public partial class TransactionNumberMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransactionNumber",
                table: "EscrowTransactions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionNumber",
                table: "EscrowTransactions");
        }
    }
}
