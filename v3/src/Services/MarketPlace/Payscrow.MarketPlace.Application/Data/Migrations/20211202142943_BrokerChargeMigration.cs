using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.MarketPlace.Application.Data.Migrations
{
    public partial class BrokerChargeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Brokers_BrokerId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Customers_CustomerId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Merchants_MerchantId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Brokers");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Merchants");

            migrationBuilder.DropTable(
                name: "PaymentAttemptLogs");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CustomerId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_MerchantId",
                table: "Transactions");

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

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "ChargeConfigs");

            migrationBuilder.RenameColumn(
                name: "MerchantId",
                table: "Transactions",
                newName: "MerchantAccountId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Transactions",
                newName: "UpdateUserId");

            migrationBuilder.RenameColumn(
                name: "BrokerId",
                table: "Transactions",
                newName: "CurrencyId");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Transactions",
                newName: "CreateUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_BrokerId",
                table: "Transactions",
                newName: "IX_Transactions_CurrencyId");

            migrationBuilder.AddColumn<Guid>(
                name: "BrokerAccountId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "BrokerFee",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "BrokerName",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateUtc",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerAccountId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CustomerCharge",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "GrandTotalPayable",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MerchantCharge",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "MerchantEmailAddress",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MerchantName",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateUtc",
                table: "Transactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "Payments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BrokerConfigs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChargeMethod = table.Column<int>(type: "int", nullable: false),
                    ChargeCap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FixedRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrokerConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("417a885f-85f9-41ae-8ccd-adf200ff59bb"), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), 200m, 100000m, 0m, 1m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("87905643-d80f-430e-8d9a-adf200ff59bb"), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), 2000m, 792281625142643m, 100000m, 0.5m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.InsertData(
                table: "ChargeConfigs",
                columns: new[] { "Id", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("9bb93fc3-fa50-41cd-bac9-adf200ff59bb"), new Guid("1c9695e0-330b-4b5b-a866-edd0efe674fa"), 2000m, 79228162514264m, 0m, 0.5m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Number",
                table: "Transactions",
                column: "Number",
                unique: true,
                filter: "[Number] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_TenantId",
                table: "Currencies",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_TransactionId",
                table: "Items",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Currencies_CurrencyId",
                table: "Transactions",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Currencies_CurrencyId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "BrokerConfigs");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_Number",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_TenantId",
                table: "Currencies");

            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("417a885f-85f9-41ae-8ccd-adf200ff59bb"));

            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("87905643-d80f-430e-8d9a-adf200ff59bb"));

            migrationBuilder.DeleteData(
                table: "ChargeConfigs",
                keyColumn: "Id",
                keyValue: new Guid("9bb93fc3-fa50-41cd-bac9-adf200ff59bb"));

            migrationBuilder.DropColumn(
                name: "BrokerAccountId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "BrokerFee",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "BrokerName",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CreateUtc",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CustomerAccountId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CustomerCharge",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "GrandTotalPayable",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "MerchantCharge",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "MerchantEmailAddress",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "MerchantName",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UpdateUtc",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "UpdateUserId",
                table: "Transactions",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "MerchantAccountId",
                table: "Transactions",
                newName: "MerchantId");

            migrationBuilder.RenameColumn(
                name: "CurrencyId",
                table: "Transactions",
                newName: "BrokerId");

            migrationBuilder.RenameColumn(
                name: "CreateUserId",
                table: "Transactions",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_CurrencyId",
                table: "Transactions",
                newName: "IX_Transactions_BrokerId");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Currencies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "ChargeConfigs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Brokers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brokers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentAttemptLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentAttemptLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentAttemptLogs_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomerId",
                table: "Transactions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_MerchantId",
                table: "Transactions",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAttemptLogs_TransactionId",
                table: "PaymentAttemptLogs",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Brokers_BrokerId",
                table: "Transactions",
                column: "BrokerId",
                principalTable: "Brokers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Customers_CustomerId",
                table: "Transactions",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Merchants_MerchantId",
                table: "Transactions",
                column: "MerchantId",
                principalTable: "Merchants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
