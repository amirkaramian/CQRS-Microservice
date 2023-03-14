using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.PaymentInvite.Data.Migrations
{
    public partial class PersonMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buyers");

            migrationBuilder.DropTable(
                name: "Sellers");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_BuyerPhone",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_SellerPhone",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "BuyerPhone",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "SellerPhone",
                table: "Transactions");

            migrationBuilder.AddColumn<Guid>(
                name: "BuyerId",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyId",
                table: "Transactions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SellerId",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuyerPhone_CountryCode",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuyerPhone_LocalNumber",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerPhone_CountryCode",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerPhone_LocalNumber",
                table: "Transactions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateUtc = table.Column<DateTime>(nullable: false),
                    UpdateUtc = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Symbol = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateUtc = table.Column<DateTime>(nullable: false),
                    UpdateUtc = table.Column<DateTime>(nullable: false),
                    IdentityGuid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PhoneNumber_CountryCode = table.Column<string>(nullable: true),
                    PhoneNumber_LocalNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BuyerId",
                table: "Transactions",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CurrencyId",
                table: "Transactions",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PersonId",
                table: "Transactions",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SellerId",
                table: "Transactions",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Code",
                table: "Currencies",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Person_IdentityGuid",
                table: "Person",
                column: "IdentityGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Person_BuyerId",
                table: "Transactions",
                column: "BuyerId",
                principalTable: "Person",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Person_BuyerId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Currencies_CurrencyId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Person_PersonId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Person_SellerId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_BuyerId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CurrencyId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_PersonId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_SellerId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "BuyerPhone_CountryCode",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "BuyerPhone_LocalNumber",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "SellerPhone_CountryCode",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "SellerPhone_LocalNumber",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "BuyerPhone",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerPhone",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Buyers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdentityGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buyers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sellers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdentityGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sellers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BuyerPhone",
                table: "Transactions",
                column: "BuyerPhone");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SellerPhone",
                table: "Transactions",
                column: "SellerPhone");
        }
    }
}
