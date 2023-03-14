using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.Payments.Api.Data.Migrations
{
    public partial class PaymentTransactionRef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentMethods_PaymentMethodId",
                table: "Payments");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionGuid",
                table: "Payments",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PaymentMethodId",
                table: "Payments",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "ExternalTransactionRef",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentMethods_PaymentMethodId",
                table: "Payments",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentMethods_PaymentMethodId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ExternalTransactionRef",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Payments");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionGuid",
                table: "Payments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "PaymentMethodId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentMethods_PaymentMethodId",
                table: "Payments",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
