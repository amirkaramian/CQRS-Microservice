using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.PaymentInvite.Data.Migrations
{
    public partial class CurrencyChargeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChargeType",
                table: "Currencies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "FixedCharge",
                table: "Currencies",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PercentageCharge",
                table: "Currencies",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                columns: new[] { "ChargeType", "FixedCharge", "PercentageCharge" },
                values: new object[] { 3, 100m, 1.5m });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("1c9695e0-330b-4b5b-a866-edd0efe674fa"),
                columns: new[] { "ChargeType", "FixedCharge", "PercentageCharge" },
                values: new object[] { 2, 5m, 1m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargeType",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "FixedCharge",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "PercentageCharge",
                table: "Currencies");
        }
    }
}
