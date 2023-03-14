using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.PaymentInvite.Data.Migrations
{
    public partial class DealsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Traders_BuyerId",
                table: "Invites");

            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Currencies_CurrencyId",
                table: "Invites");

            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Traders_SellerId",
                table: "Invites");

            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Traders_TraderId",
                table: "Invites");

            migrationBuilder.DropTable(
                name: "TradeItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invites",
                table: "Invites");

            migrationBuilder.DropIndex(
                name: "IX_Invites_CurrencyId",
                table: "Invites");

            migrationBuilder.DropIndex(
                name: "IX_Invites_SellerEmail",
                table: "Invites");

            migrationBuilder.DropIndex(
                name: "IX_Invites_SellerId",
                table: "Invites");

            migrationBuilder.DropColumn(
                name: "BuyerResponseLink",
                table: "Invites");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Invites");

            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "Invites");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Invites");

            migrationBuilder.DropColumn(
                name: "SellerEmail",
                table: "Invites");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Invites");

            migrationBuilder.DropColumn(
                name: "SellerVerificationCode",
                table: "Invites");

            migrationBuilder.DropColumn(
                name: "SellerPhone_CountryCode",
                table: "Invites");

            migrationBuilder.DropColumn(
                name: "SellerPhone_LocalNumber",
                table: "Invites");

            migrationBuilder.RenameTable(
                name: "Invites",
                newName: "Transactions");

            migrationBuilder.RenameIndex(
                name: "IX_Invites_TraderId",
                table: "Transactions",
                newName: "IX_Transactions_TraderId");

            migrationBuilder.RenameIndex(
                name: "IX_Invites_BuyerId",
                table: "Transactions",
                newName: "IX_Transactions_BuyerId");

            migrationBuilder.RenameIndex(
                name: "IX_Invites_BuyerEmail",
                table: "Transactions",
                newName: "IX_Transactions_BuyerEmail");

            migrationBuilder.AddColumn<Guid>(
                name: "DealId",
                table: "Transactions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DealId1",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Deals",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateUtc = table.Column<DateTime>(nullable: false),
                    UpdateUtc = table.Column<DateTime>(nullable: false),
                    SellerEmail = table.Column<string>(nullable: true),
                    SellerPhone_CountryCode = table.Column<string>(nullable: true),
                    SellerPhone_LocalNumber = table.Column<string>(nullable: true),
                    SellerChargePercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellerVerificationCode = table.Column<string>(nullable: true),
                    BuyerLink = table.Column<string>(nullable: true),
                    IsVerified = table.Column<bool>(nullable: false),
                    CurrencyId = table.Column<Guid>(nullable: false),
                    SellerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deals_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deals_Traders_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Traders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DealItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateUtc = table.Column<DateTime>(nullable: false),
                    UpdateUtc = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AvailableQuantity = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DealId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DealItems_Deals_DealId",
                        column: x => x.DealId,
                        principalTable: "Deals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateUtc = table.Column<DateTime>(nullable: false),
                    UpdateUtc = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    DealItemId = table.Column<Guid>(nullable: true),
                    TransactionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionItems_DealItems_DealItemId",
                        column: x => x.DealItemId,
                        principalTable: "DealItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionItems_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DealId",
                table: "Transactions",
                column: "DealId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DealId1",
                table: "Transactions",
                column: "DealId1");

            migrationBuilder.CreateIndex(
                name: "IX_DealItems_DealId",
                table: "DealItems",
                column: "DealId");

            migrationBuilder.CreateIndex(
                name: "IX_Deals_CurrencyId",
                table: "Deals",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Deals_SellerId",
                table: "Deals",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionItems_DealItemId",
                table: "TransactionItems",
                column: "DealItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionItems_TransactionId",
                table: "TransactionItems",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Traders_BuyerId",
                table: "Transactions",
                column: "BuyerId",
                principalTable: "Traders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Deals_DealId",
                table: "Transactions",
                column: "DealId",
                principalTable: "Deals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Deals_DealId1",
                table: "Transactions",
                column: "DealId1",
                principalTable: "Deals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Traders_TraderId",
                table: "Transactions",
                column: "TraderId",
                principalTable: "Traders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Traders_BuyerId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Deals_DealId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Deals_DealId1",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Traders_TraderId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "TransactionItems");

            migrationBuilder.DropTable(
                name: "DealItems");

            migrationBuilder.DropTable(
                name: "Deals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_DealId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_DealId1",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DealId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DealId1",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Invites");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_TraderId",
                table: "Invites",
                newName: "IX_Invites_TraderId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_BuyerId",
                table: "Invites",
                newName: "IX_Invites_BuyerId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_BuyerEmail",
                table: "Invites",
                newName: "IX_Invites_BuyerEmail");

            migrationBuilder.AddColumn<string>(
                name: "BuyerResponseLink",
                table: "Invites",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyId",
                table: "Invites",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "Invites",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Invites",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SellerEmail",
                table: "Invites",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SellerId",
                table: "Invites",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerVerificationCode",
                table: "Invites",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerPhone_CountryCode",
                table: "Invites",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerPhone_LocalNumber",
                table: "Invites",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invites",
                table: "Invites",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TradeItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InviteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UpdateUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradeItems_Invites_InviteId",
                        column: x => x.InviteId,
                        principalTable: "Invites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invites_CurrencyId",
                table: "Invites",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invites_SellerEmail",
                table: "Invites",
                column: "SellerEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Invites_SellerId",
                table: "Invites",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeItems_InviteId",
                table: "TradeItems",
                column: "InviteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invites_Traders_BuyerId",
                table: "Invites",
                column: "BuyerId",
                principalTable: "Traders",
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
    }
}
