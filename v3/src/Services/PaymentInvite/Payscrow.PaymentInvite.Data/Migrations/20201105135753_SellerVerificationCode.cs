using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.PaymentInvite.Data.Migrations
{
    public partial class SellerVerificationCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Trader_BuyerId",
                table: "Invites");

            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Trader_SellerId",
                table: "Invites");

            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Trader_TraderId",
                table: "Invites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trader",
                table: "Trader");

            migrationBuilder.RenameTable(
                name: "Trader",
                newName: "Traders");

            migrationBuilder.RenameIndex(
                name: "IX_Trader_IdentityGuid",
                table: "Traders",
                newName: "IX_Traders_IdentityGuid");

            migrationBuilder.AddColumn<string>(
                name: "SellerVerificationCode",
                table: "Invites",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Traders",
                table: "Traders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invites_Traders_BuyerId",
                table: "Invites",
                column: "BuyerId",
                principalTable: "Traders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invites_Traders_SellerId",
                table: "Invites",
                column: "SellerId",
                principalTable: "Traders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invites_Traders_TraderId",
                table: "Invites",
                column: "TraderId",
                principalTable: "Traders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Traders_BuyerId",
                table: "Invites");

            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Traders_SellerId",
                table: "Invites");

            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Traders_TraderId",
                table: "Invites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Traders",
                table: "Traders");

            migrationBuilder.DropColumn(
                name: "SellerVerificationCode",
                table: "Invites");

            migrationBuilder.RenameTable(
                name: "Traders",
                newName: "Trader");

            migrationBuilder.RenameIndex(
                name: "IX_Traders_IdentityGuid",
                table: "Trader",
                newName: "IX_Trader_IdentityGuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trader",
                table: "Trader",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invites_Trader_BuyerId",
                table: "Invites",
                column: "BuyerId",
                principalTable: "Trader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invites_Trader_SellerId",
                table: "Invites",
                column: "SellerId",
                principalTable: "Trader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invites_Trader_TraderId",
                table: "Invites",
                column: "TraderId",
                principalTable: "Trader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
