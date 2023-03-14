using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.MarketPlace.Application.Data.Migrations
{
    public partial class TransactionStatusLogCreateDateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("2b6fc176-f2c3-4a67-9327-ae50017e102b"));

            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("8da20f77-2b7e-407a-a6fc-ae50017e102b"));

            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("e5eb5dd0-edee-4c3c-8127-ae50017e102b"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateUtc",
                table: "TransactionStatusLogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateUtc",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("fb5015b5-e1a7-46fd-818c-ae50017f6f16"), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), 200m, 100000m, 0m, 1m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("c26d7097-51be-4e3b-88a9-ae50017f6f16"), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), 2000m, 792281625142643m, 100000m, 0.5m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("3108ae68-ec2c-467b-af6b-ae50017f6f16"), new Guid("1c9695e0-330b-4b5b-a866-edd0efe674fa"), 2000m, 79228162514264m, 0m, 0.5m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("3108ae68-ec2c-467b-af6b-ae50017f6f16"));

            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("c26d7097-51be-4e3b-88a9-ae50017f6f16"));

            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("fb5015b5-e1a7-46fd-818c-ae50017f6f16"));

            migrationBuilder.DropColumn(
                name: "CreateUtc",
                table: "TransactionStatusLogs");

            migrationBuilder.DropColumn(
                name: "CreateUtc",
                table: "Transactions");

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("e5eb5dd0-edee-4c3c-8127-ae50017e102b"), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), 200m, 100000m, 0m, 1m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("2b6fc176-f2c3-4a67-9327-ae50017e102b"), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), 2000m, 792281625142643m, 100000m, 0.5m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("8da20f77-2b7e-407a-a6fc-ae50017e102b"), new Guid("1c9695e0-330b-4b5b-a866-edd0efe674fa"), 2000m, 79228162514264m, 0m, 0.5m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });
        }
    }
}
