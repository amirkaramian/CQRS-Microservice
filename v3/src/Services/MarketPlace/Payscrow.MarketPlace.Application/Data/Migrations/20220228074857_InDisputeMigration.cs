using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.MarketPlace.Application.Data.Migrations
{
    public partial class InDisputeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("b8425b75-a7f8-4223-96c5-ae4701151ac9"));

            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("c7c481da-eca1-4c69-afc3-ae4701151ac9"));

            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("df05cb22-8c59-4985-90c2-ae4701151ac9"));

            migrationBuilder.RenameColumn(
                name: "IsEscrow",
                table: "Transactions",
                newName: "InEscrow");

            migrationBuilder.AddColumn<bool>(
                name: "InDispute",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("4c099386-ec96-4d1e-83d4-ae4a009147a1"), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), 200m, 100000m, 0m, 1m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("3e244e82-e21d-47a8-b81e-ae4a009147a1"), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), 2000m, 792281625142643m, 100000m, 0.5m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("46488e71-7fbf-4ffd-ae06-ae4a009147a1"), new Guid("1c9695e0-330b-4b5b-a866-edd0efe674fa"), 2000m, 79228162514264m, 0m, 0.5m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("3e244e82-e21d-47a8-b81e-ae4a009147a1"));

            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("46488e71-7fbf-4ffd-ae06-ae4a009147a1"));

            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("4c099386-ec96-4d1e-83d4-ae4a009147a1"));

            migrationBuilder.DropColumn(
                name: "InDispute",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "InEscrow",
                table: "Transactions",
                newName: "IsEscrow");

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("c7c481da-eca1-4c69-afc3-ae4701151ac9"), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), 200m, 100000m, 0m, 1m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("df05cb22-8c59-4985-90c2-ae4701151ac9"), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), 2000m, 792281625142643m, 100000m, 0.5m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("b8425b75-a7f8-4223-96c5-ae4701151ac9"), new Guid("1c9695e0-330b-4b5b-a866-edd0efe674fa"), 2000m, 79228162514264m, 0m, 0.5m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });
        }
    }
}
