using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.Escrow.Data.Migrations
{
    public partial class EscrowTransactionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "EscrowTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    CreateUtc = table.Column<DateTime>(nullable: false),
                    UpdateUtc = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsReleased = table.Column<bool>(nullable: false),
                    CurrencyId = table.Column<Guid>(nullable: false),
                    TransactionGuid = table.Column<Guid>(nullable: false),
                    ServiceTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscrowTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EscrowTransactionAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    CreateUtc = table.Column<DateTime>(nullable: false),
                    UpdateUtc = table.Column<DateTime>(nullable: false),
                    EscrowTransactionId = table.Column<Guid>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscrowTransactionAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EscrowTransactionAccounts_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EscrowTransactionAccounts_EscrowTransactions_EscrowTransactionId",
                        column: x => x.EscrowTransactionId,
                        principalTable: "EscrowTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                column: "TenantId",
                value: new Guid("30867e39-acca-4565-b5e5-c3785b6f8899"));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("1c9695e0-330b-4b5b-a866-edd0efe674fa"),
                column: "TenantId",
                value: new Guid("30867e39-acca-4565-b5e5-c3785b6f8899"));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("9824e994-7b7d-439a-8322-a07a4b64165d"),
                column: "TenantId",
                value: new Guid("30867e39-acca-4565-b5e5-c3785b6f8899"));

            migrationBuilder.CreateIndex(
                name: "IX_EscrowTransactionAccounts_AccountId",
                table: "EscrowTransactionAccounts",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_EscrowTransactionAccounts_EscrowTransactionId",
                table: "EscrowTransactionAccounts",
                column: "EscrowTransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EscrowTransactionAccounts");

            migrationBuilder.DropTable(
                name: "EscrowTransactions");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                column: "TenantId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("1c9695e0-330b-4b5b-a866-edd0efe674fa"),
                column: "TenantId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("9824e994-7b7d-439a-8322-a07a4b64165d"),
                column: "TenantId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
