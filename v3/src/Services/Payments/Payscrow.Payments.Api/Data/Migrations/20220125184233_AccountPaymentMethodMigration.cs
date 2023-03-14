using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.Payments.Api.Data.Migrations
{
    public partial class AccountPaymentMethodMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Payments",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "PaymentMethods",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "PaymentMethodCurrencies",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Currencies",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AccountPaymentMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    CreateUtc = table.Column<DateTime>(nullable: false),
                    UpdateUtc = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: false),
                    PaymentMethodId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountPaymentMethods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountPaymentMethods_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountPaymentMethods_PaymentMethodId",
                table: "AccountPaymentMethods",
                column: "PaymentMethodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountPaymentMethods");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "PaymentMethodCurrencies");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Currencies");
        }
    }
}
