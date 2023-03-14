using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.MarketPlace.Application.Data.Migrations
{
    public partial class BrokerReferenceMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("a4d48218-db5f-413a-8463-ae260108a42c"));

            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("bd1d860b-286a-4c82-a0e0-ae260108a42c"));

            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("ee01d1a2-f736-4c58-ac6d-ae260108a42c"));

            migrationBuilder.AddColumn<string>(
                name: "BrokerTransactionReference",
                table: "Transactions",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BrokerTransactionReference",
                table: "Transactions",
                column: "BrokerTransactionReference");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transactions_BrokerTransactionReference",
                table: "Transactions");

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

            migrationBuilder.DropColumn(
                name: "BrokerTransactionReference",
                table: "Transactions");

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("bd1d860b-286a-4c82-a0e0-ae260108a42c"), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), 200m, 100000m, 0m, 1m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("a4d48218-db5f-413a-8463-ae260108a42c"), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), 2000m, 792281625142643m, 100000m, 0.5m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("ee01d1a2-f736-4c58-ac6d-ae260108a42c"), new Guid("1c9695e0-330b-4b5b-a866-edd0efe674fa"), 2000m, 79228162514264m, 0m, 0.5m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });
        }
    }
}
