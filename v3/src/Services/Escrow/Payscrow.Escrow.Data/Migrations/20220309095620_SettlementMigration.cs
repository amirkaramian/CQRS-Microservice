using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.Escrow.Data.Migrations
{
    public partial class SettlementMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerAccountId",
                table: "EscrowTransactions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PayerAccountId",
                table: "EscrowTransactions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "EscrowTransactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "EscrowTransactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Settlements",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    CreateUtc = table.Column<DateTime>(nullable: false),
                    UpdateUtc = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    BankAccountName = table.Column<string>(nullable: true),
                    BankAccountNumber = table.Column<string>(nullable: true),
                    BankCode = table.Column<string>(nullable: true),
                    BankName = table.Column<string>(nullable: true),
                    EscrowTransactionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settlements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settlements_EscrowTransactions_EscrowTransactionId",
                        column: x => x.EscrowTransactionId,
                        principalTable: "EscrowTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_EscrowTransactionId",
                table: "Settlements",
                column: "EscrowTransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settlements");

            migrationBuilder.DropColumn(
                name: "OwnerAccountId",
                table: "EscrowTransactions");

            migrationBuilder.DropColumn(
                name: "PayerAccountId",
                table: "EscrowTransactions");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "EscrowTransactions");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "EscrowTransactions");
        }
    }
}
