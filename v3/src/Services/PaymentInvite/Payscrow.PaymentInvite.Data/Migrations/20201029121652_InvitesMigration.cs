using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.PaymentInvite.Data.Migrations
{
    public partial class InvitesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionItems_Transactions_InviteId",
                table: "TransactionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Trader_BuyerId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Currencies_CurrencyId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Trader_SellerId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Trader_TraderId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionItems",
                table: "TransactionItems");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Invites");

            migrationBuilder.RenameTable(
                name: "TransactionItems",
                newName: "TradeItems");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_TraderId",
                table: "Invites",
                newName: "IX_Invites_TraderId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_SellerId",
                table: "Invites",
                newName: "IX_Invites_SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_SellerEmail",
                table: "Invites",
                newName: "IX_Invites_SellerEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_CurrencyId",
                table: "Invites",
                newName: "IX_Invites_CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_BuyerId",
                table: "Invites",
                newName: "IX_Invites_BuyerId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_BuyerEmail",
                table: "Invites",
                newName: "IX_Invites_BuyerEmail");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionItems_InviteId",
                table: "TradeItems",
                newName: "IX_TradeItems_InviteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invites",
                table: "Invites",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TradeItems",
                table: "TradeItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invites_Trader_BuyerId",
                table: "Invites",
                column: "BuyerId",
                principalTable: "Trader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invites_Currencies_CurrencyId",
                table: "Invites",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_TradeItems_Invites_InviteId",
                table: "TradeItems",
                column: "InviteId",
                principalTable: "Invites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Trader_BuyerId",
                table: "Invites");

            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Currencies_CurrencyId",
                table: "Invites");

            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Trader_SellerId",
                table: "Invites");

            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Trader_TraderId",
                table: "Invites");

            migrationBuilder.DropForeignKey(
                name: "FK_TradeItems_Invites_InviteId",
                table: "TradeItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TradeItems",
                table: "TradeItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invites",
                table: "Invites");

            migrationBuilder.RenameTable(
                name: "TradeItems",
                newName: "TransactionItems");

            migrationBuilder.RenameTable(
                name: "Invites",
                newName: "Transactions");

            migrationBuilder.RenameIndex(
                name: "IX_TradeItems_InviteId",
                table: "TransactionItems",
                newName: "IX_TransactionItems_InviteId");

            migrationBuilder.RenameIndex(
                name: "IX_Invites_TraderId",
                table: "Transactions",
                newName: "IX_Transactions_TraderId");

            migrationBuilder.RenameIndex(
                name: "IX_Invites_SellerId",
                table: "Transactions",
                newName: "IX_Transactions_SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Invites_SellerEmail",
                table: "Transactions",
                newName: "IX_Transactions_SellerEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Invites_CurrencyId",
                table: "Transactions",
                newName: "IX_Transactions_CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Invites_BuyerId",
                table: "Transactions",
                newName: "IX_Transactions_BuyerId");

            migrationBuilder.RenameIndex(
                name: "IX_Invites_BuyerEmail",
                table: "Transactions",
                newName: "IX_Transactions_BuyerEmail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionItems",
                table: "TransactionItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionItems_Transactions_InviteId",
                table: "TransactionItems",
                column: "InviteId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Trader_BuyerId",
                table: "Transactions",
                column: "BuyerId",
                principalTable: "Trader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Currencies_CurrencyId",
                table: "Transactions",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Trader_SellerId",
                table: "Transactions",
                column: "SellerId",
                principalTable: "Trader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Trader_TraderId",
                table: "Transactions",
                column: "TraderId",
                principalTable: "Trader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
