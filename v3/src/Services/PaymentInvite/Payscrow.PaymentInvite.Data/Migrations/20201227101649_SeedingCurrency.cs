using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.PaymentInvite.Data.Migrations
{
    public partial class SeedingCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "CreateUtc", "IsActive", "Name", "Symbol", "UpdateUtc" },
                values: new object[] { new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), "NGN", new DateTime(2020, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Naira", "N", new DateTime(2020, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "CreateUtc", "IsActive", "Name", "Symbol", "UpdateUtc" },
                values: new object[] { new Guid("1c9695e0-330b-4b5b-a866-edd0efe674fa"), "USD", new DateTime(2020, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "US Dollar", "$", new DateTime(2020, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("1c9695e0-330b-4b5b-a866-edd0efe674fa"));
        }
    }
}
