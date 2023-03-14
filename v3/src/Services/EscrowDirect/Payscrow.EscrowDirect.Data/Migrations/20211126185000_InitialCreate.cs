using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.EscrowDirect.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ChargeMethod = table.Column<int>(type: "int", nullable: false),
                    ChargeCap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerChargePercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ChargeFixedRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChargePercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChargeCap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UseMerchantRates = table.Column<bool>(type: "bit", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChargeConfigurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaxTransactionAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinTransactionAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FixedRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargeConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChargeConfigurations_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsEscrow = table.Column<bool>(type: "bit", nullable: false),
                    TotalChargeInclusive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MerchantCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MerchantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Merchants_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "Merchants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "PaymentAttemptLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "AccountId", "ChargeCap", "ChargeMethod", "Code", "IsActive", "Name", "Order", "Symbol", "TenantId" },
                values: new object[] { new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), new Guid("00000000-0000-0000-0000-000000000000"), 2000m, 15, "NGN", true, "Naira", 1, "N", new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "AccountId", "ChargeCap", "ChargeMethod", "Code", "IsActive", "Name", "Order", "Symbol", "TenantId" },
                values: new object[] { new Guid("1c9695e0-330b-4b5b-a866-edd0efe674fa"), new Guid("00000000-0000-0000-0000-000000000000"), 100m, 5, "USD", false, "US Dollar", 2, "$", new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.InsertData(
                table: "ChargeConfigurations",
                columns: new[] { "Id", "AccountId", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("e80d0844-17c1-4869-aa90-adec0146d7b0"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), 200m, 100000m, 0m, 1m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.InsertData(
                table: "ChargeConfigurations",
                columns: new[] { "Id", "AccountId", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("92f9f5ca-1a20-4ecf-b741-adec0146d7b0"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), 2000m, 792281625142643m, 100000m, 0.5m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.InsertData(
                table: "ChargeConfigurations",
                columns: new[] { "Id", "AccountId", "CurrencyId", "FixedRate", "MaxTransactionAmount", "MinTransactionAmount", "Percentage", "TenantId" },
                values: new object[] { new Guid("f9cfe12e-a6b2-43f6-b44f-adec0146d7b0"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("1c9695e0-330b-4b5b-a866-edd0efe674fa"), 2000m, 79228162514264m, 0m, 0.5m, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.CreateIndex(
                name: "IX_ChargeConfigurations_CurrencyId",
                table: "ChargeConfigurations",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ChargeConfigurations_MaxTransactionAmount",
                table: "ChargeConfigurations",
                column: "MaxTransactionAmount");

            migrationBuilder.CreateIndex(
                name: "IX_ChargeConfigurations_MinTransactionAmount",
                table: "ChargeConfigurations",
                column: "MinTransactionAmount");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Code",
                table: "Currencies",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Items_TransactionId",
                table: "Items",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAttemptLogs_TransactionId",
                table: "PaymentAttemptLogs",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Number",
                table: "Payments",
                column: "Number");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TransactionId",
                table: "Payments",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CurrencyId",
                table: "Transactions",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_MerchantId",
                table: "Transactions",
                column: "MerchantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChargeConfigurations");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "PaymentAttemptLogs");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Merchants");
        }
    }
}
