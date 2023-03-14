using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.Escrow.Data.Migrations
{
    public partial class AccountSettingMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AccountSettings_AccountGuid",
                table: "AccountSettings");

            migrationBuilder.DropColumn(
                name: "AccountGuid",
                table: "AccountSettings");

            migrationBuilder.DropColumn(
                name: "DefaultCurrencyCode",
                table: "AccountSettings");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "AccountSettings",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DefaultCurrencyId",
                table: "AccountSettings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountSettings_AccountId",
                table: "AccountSettings",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSettings_DefaultCurrencyId",
                table: "AccountSettings",
                column: "DefaultCurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSettings_Accounts_AccountId",
                table: "AccountSettings",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSettings_Currencies_DefaultCurrencyId",
                table: "AccountSettings",
                column: "DefaultCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountSettings_Accounts_AccountId",
                table: "AccountSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountSettings_Currencies_DefaultCurrencyId",
                table: "AccountSettings");

            migrationBuilder.DropIndex(
                name: "IX_AccountSettings_AccountId",
                table: "AccountSettings");

            migrationBuilder.DropIndex(
                name: "IX_AccountSettings_DefaultCurrencyId",
                table: "AccountSettings");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "AccountSettings");

            migrationBuilder.DropColumn(
                name: "DefaultCurrencyId",
                table: "AccountSettings");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountGuid",
                table: "AccountSettings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "DefaultCurrencyCode",
                table: "AccountSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountSettings_AccountGuid",
                table: "AccountSettings",
                column: "AccountGuid",
                unique: true);
        }
    }
}
