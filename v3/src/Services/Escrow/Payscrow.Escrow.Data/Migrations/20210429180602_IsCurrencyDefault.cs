using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.Escrow.Data.Migrations
{
    public partial class IsCurrencyDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Currencies",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                column: "IsDefault",
                value: true);

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "CreateUtc", "IsActive", "IsDefault", "Name", "Order", "Symbol", "UpdateUtc" },
                values: new object[] { new Guid("9824e994-7b7d-439a-8322-a07a4b64165d"), "CFA", new DateTime(2020, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, "franc", 2, "C", new DateTime(2020, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("9824e994-7b7d-439a-8322-a07a4b64165d"));

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Currencies");
        }
    }
}
