using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.Payments.Api.Data.Migrations
{
    public partial class PaymentSettlementsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settlements",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    CreateUtc = table.Column<DateTime>(nullable: false),
                    UpdateUtc = table.Column<DateTime>(nullable: false),
                    TransactionGuid = table.Column<Guid>(nullable: false),
                    CurrencyCode = table.Column<string>(maxLength: 3, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Provider = table.Column<int>(nullable: false),
                    GatewayReference = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settlements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SettlementAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    CreateUtc = table.Column<DateTime>(nullable: false),
                    UpdateUtc = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    BankCode = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    AccountName = table.Column<string>(nullable: true),
                    GatewayReference = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    SettlementId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettlementAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettlementAccounts_Settlements_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                column: "LogoFileName",
                value: "flutterwave-logo.png");

            migrationBuilder.CreateIndex(
                name: "IX_SettlementAccounts_SettlementId",
                table: "SettlementAccounts",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_TransactionGuid",
                table: "Settlements",
                column: "TransactionGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SettlementAccounts");

            migrationBuilder.DropTable(
                name: "Settlements");

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                column: "LogoFileName",
                value: null);
        }
    }
}
