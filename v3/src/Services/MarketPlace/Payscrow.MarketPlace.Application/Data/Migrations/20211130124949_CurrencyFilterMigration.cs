using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.MarketPlace.Application.Data.Migrations
{
    public partial class CurrencyFilterMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("12be2f05-ef81-443a-9539-adf0009fa8a1"));

            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("6b4d3838-c904-4637-aa84-adf0009fa8a1"));

            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("825fb602-365e-4054-a4bb-adf0009fa8a1"));

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "AccountId", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("d549d99d-8e80-4d5b-b929-adf000e3e916"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), 200m, 100000m, 0m, 1m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "AccountId", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("d4f48784-83a5-4835-b23e-adf000e3e916"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), 2000m, 792281625142643m, 100000m, 0.5m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "AccountId", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("e2bb4b37-6683-4e97-aca0-adf000e3e916"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("1c9695e0-330b-4b5b-a866-edd0efe674fa"), 2000m, 79228162514264m, 0m, 0.5m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("d4f48784-83a5-4835-b23e-adf000e3e916"));

            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("d549d99d-8e80-4d5b-b929-adf000e3e916"));

            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("e2bb4b37-6683-4e97-aca0-adf000e3e916"));

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "AccountId", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("12be2f05-ef81-443a-9539-adf0009fa8a1"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), 200m, 100000m, 0m, 1m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "AccountId", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("6b4d3838-c904-4637-aa84-adf0009fa8a1"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), 2000m, 792281625142643m, 100000m, 0.5m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "AccountId", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("825fb602-365e-4054-a4bb-adf0009fa8a1"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("1c9695e0-330b-4b5b-a866-edd0efe674fa"), 2000m, 79228162514264m, 0m, 0.5m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });
        }
    }
}
