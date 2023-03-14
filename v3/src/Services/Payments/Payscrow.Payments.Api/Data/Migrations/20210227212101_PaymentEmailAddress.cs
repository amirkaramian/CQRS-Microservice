using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.Payments.Api.Data.Migrations
{
    public partial class PaymentEmailAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TransactionGuid",
                table: "Payments",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Payments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Payments",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                column: "Name",
                value: "Flutterwave");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TransactionGuid",
                table: "Payments",
                column: "TransactionGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_TransactionGuid",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Payments");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionGuid",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                column: "Name",
                value: null);
        }
    }
}
