using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.Payments.Api.Data.Migrations
{
    public partial class Currencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "CreateUtc", "Name", "UpdateUtc" },
                values: new object[] { new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), "NGN", new DateTime(2021, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Naira", new DateTime(2021, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"));
        }
    }
}
