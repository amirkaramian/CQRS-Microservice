using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.MarketPlace.Application.Data.Migrations
{
    public partial class EscrowStatusMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SettlementAccount_Transactions_TransactionId",
                table: "SettlementAccount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SettlementAccount",
                table: "SettlementAccount");

            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("90fdbaca-6fb0-474d-8de1-ae2e01688ff2"));

            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("c97cf746-cca1-4a42-9732-ae2e01688ff2"));

            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("e7428d32-c63e-4b3a-b46a-ae2e01688ff2"));

            migrationBuilder.RenameTable(
                name: "SettlementAccount",
                newName: "SettlementAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_SettlementAccount_TransactionId",
                table: "SettlementAccounts",
                newName: "IX_SettlementAccounts_TransactionId");

            migrationBuilder.AddColumn<string>(
                name: "EscrowCode",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEscrow",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SettlementAccounts",
                table: "SettlementAccounts",
                column: "Id");

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("ee053a50-43d6-4e9e-acdf-ae36013c6f4f"), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), 200m, 100000m, 0m, 1m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("b10e53b5-e6bb-49ac-b79c-ae36013c6f4f"), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), 2000m, 792281625142643m, 100000m, 0.5m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("dcdbeed7-3526-4a20-ba7e-ae36013c6f4f"), new Guid("1c9695e0-330b-4b5b-a866-edd0efe674fa"), 2000m, 79228162514264m, 0m, 0.5m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.AddForeignKey(
                name: "FK_SettlementAccounts_Transactions_TransactionId",
                table: "SettlementAccounts",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SettlementAccounts_Transactions_TransactionId",
                table: "SettlementAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SettlementAccounts",
                table: "SettlementAccounts");

            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("b10e53b5-e6bb-49ac-b79c-ae36013c6f4f"));

            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("dcdbeed7-3526-4a20-ba7e-ae36013c6f4f"));

            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("ee053a50-43d6-4e9e-acdf-ae36013c6f4f"));

            migrationBuilder.DropColumn(
                name: "EscrowCode",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "IsEscrow",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "SettlementAccounts",
                newName: "SettlementAccount");

            migrationBuilder.RenameIndex(
                name: "IX_SettlementAccounts_TransactionId",
                table: "SettlementAccount",
                newName: "IX_SettlementAccount_TransactionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SettlementAccount",
                table: "SettlementAccount",
                column: "Id");

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("e7428d32-c63e-4b3a-b46a-ae2e01688ff2"), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), 200m, 100000m, 0m, 1m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("90fdbaca-6fb0-474d-8de1-ae2e01688ff2"), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), 2000m, 792281625142643m, 100000m, 0.5m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("c97cf746-cca1-4a42-9732-ae2e01688ff2"), new Guid("1c9695e0-330b-4b5b-a866-edd0efe674fa"), 2000m, 79228162514264m, 0m, 0.5m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.AddForeignKey(
                name: "FK_SettlementAccount_Transactions_TransactionId",
                table: "SettlementAccount",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
