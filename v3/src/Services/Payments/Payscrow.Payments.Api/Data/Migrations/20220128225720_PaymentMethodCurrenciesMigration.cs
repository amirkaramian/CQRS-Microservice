using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.Payments.Api.Data.Migrations
{
    public partial class PaymentMethodCurrenciesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                column: "TenantId",
                value: new Guid("30867e39-acca-4565-b5e5-c3785b6f8899"));

            migrationBuilder.InsertData(
                table: "PaymentMethodCurrencies",
                columns: new[] { "Id", "CreateUtc", "CurrencyId", "PaymentMethodId", "TenantId", "UpdateUtc" },
                values: new object[] { new Guid("3905a4e5-1ddc-4e12-ac3f-6aa59c0ee828"), new DateTime(2021, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), new Guid("30867e39-acca-4565-b5e5-c3785b6f8899"), new DateTime(2021, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                columns: new[] { "IsActive", "Provider", "TenantId" },
                values: new object[] { true, 1, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentMethodCurrencies",
                keyColumn: "Id",
                keyValue: new Guid("3905a4e5-1ddc-4e12-ac3f-6aa59c0ee828"));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                column: "TenantId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                columns: new[] { "IsActive", "Provider", "TenantId" },
                values: new object[] { false, 0, new Guid("00000000-0000-0000-0000-000000000000") });
        }
    }
}
