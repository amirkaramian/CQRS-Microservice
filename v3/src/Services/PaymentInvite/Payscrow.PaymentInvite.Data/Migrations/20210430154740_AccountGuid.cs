using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.PaymentInvite.Data.Migrations
{
    public partial class AccountGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Traders_IdentityGuid",
                table: "Traders");

            migrationBuilder.DropColumn(
                name: "IdentityGuid",
                table: "Traders");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountGuid",
                table: "Traders",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Traders_AccountGuid",
                table: "Traders",
                column: "AccountGuid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Traders_AccountGuid",
                table: "Traders");

            migrationBuilder.DropColumn(
                name: "AccountGuid",
                table: "Traders");

            migrationBuilder.AddColumn<Guid>(
                name: "IdentityGuid",
                table: "Traders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Traders_IdentityGuid",
                table: "Traders",
                column: "IdentityGuid");
        }
    }
}
