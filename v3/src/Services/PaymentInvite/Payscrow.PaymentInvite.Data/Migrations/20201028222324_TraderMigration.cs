using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.PaymentInvite.Data.Migrations
{
    public partial class TraderMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionItems_Transactions_TransactionId",
                table: "TransactionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Person_BuyerId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Person_PersonId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Person_SellerId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_PersonId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_TransactionItems_TransactionId",
                table: "TransactionItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "TransactionItems");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "Trader");

            migrationBuilder.RenameIndex(
                name: "IX_Person_IdentityGuid",
                table: "Trader",
                newName: "IX_Trader_IdentityGuid");

            migrationBuilder.AddColumn<Guid>(
                name: "TraderId",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InviteId",
                table: "TransactionItems",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Currencies",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trader",
                table: "Trader",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TraderId",
                table: "Transactions",
                column: "TraderId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionItems_InviteId",
                table: "TransactionItems",
                column: "InviteId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionItems_Transactions_InviteId",
                table: "TransactionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Trader_BuyerId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Trader_SellerId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Trader_TraderId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_TraderId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_TransactionItems_InviteId",
                table: "TransactionItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trader",
                table: "Trader");

            migrationBuilder.DropColumn(
                name: "TraderId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "InviteId",
                table: "TransactionItems");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Currencies");

            migrationBuilder.RenameTable(
                name: "Trader",
                newName: "Person");

            migrationBuilder.RenameIndex(
                name: "IX_Trader_IdentityGuid",
                table: "Person",
                newName: "IX_Person_IdentityGuid");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionId",
                table: "TransactionItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Person",
                table: "Person",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PersonId",
                table: "Transactions",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionItems_TransactionId",
                table: "TransactionItems",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionItems_Transactions_TransactionId",
                table: "TransactionItems",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Person_BuyerId",
                table: "Transactions",
                column: "BuyerId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Person_PersonId",
                table: "Transactions",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Person_SellerId",
                table: "Transactions",
                column: "SellerId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
