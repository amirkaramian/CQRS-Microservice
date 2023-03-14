using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.Escrow.Data.Migrations
{
    public partial class AccountSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_UserGuid",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "UserGuid",
                table: "Accounts");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountGuid",
                table: "Accounts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Accounts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccountSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateUtc = table.Column<DateTime>(nullable: false),
                    UpdateUtc = table.Column<DateTime>(nullable: false),
                    AccountGuid = table.Column<Guid>(nullable: false),
                    DefaultCurrencyCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSettings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountGuid",
                table: "Accounts",
                column: "AccountGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountSettings_AccountGuid",
                table: "AccountSettings",
                column: "AccountGuid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountSettings");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_AccountGuid",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AccountGuid",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Accounts");

            migrationBuilder.AddColumn<Guid>(
                name: "UserGuid",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserGuid",
                table: "Accounts",
                column: "UserGuid",
                unique: true);
        }
    }
}
