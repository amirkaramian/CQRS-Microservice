using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.Payments.Api.Data.Migrations
{
    public partial class InterswitchMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "CreateUtc", "IsActive", "LogoFileName", "Name", "Provider", "TenantId", "UpdateUtc" },
                values: new object[] { new Guid("58015b76-1512-492e-8c33-d54f888b7868"), new DateTime(2022, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "interswitch-logo.png", "Interswitch", 3, new Guid("30867e39-acca-4565-b5e5-c3785b6f8899"), new DateTime(2022, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "PaymentMethodCurrencies",
                columns: new[] { "Id", "CreateUtc", "CurrencyId", "PaymentMethodId", "TenantId", "UpdateUtc" },
                values: new object[] { new Guid("5f3548b2-2534-4bdf-9d48-7c672085aca6"), new DateTime(2022, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), new Guid("58015b76-1512-492e-8c33-d54f888b7868"), new Guid("30867e39-acca-4565-b5e5-c3785b6f8899"), new DateTime(2022, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentMethodCurrencies",
                keyColumn: "Id",
                keyValue: new Guid("5f3548b2-2534-4bdf-9d48-7c672085aca6"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("58015b76-1512-492e-8c33-d54f888b7868"));
        }
    }
}
