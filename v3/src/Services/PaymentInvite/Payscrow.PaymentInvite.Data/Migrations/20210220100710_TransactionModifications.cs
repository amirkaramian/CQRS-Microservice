using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.PaymentInvite.Data.Migrations
{
    public partial class TransactionModifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InEscrow",
                table: "Transactions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "TransactionItems",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TransactionItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Deals",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Deals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Deals",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateUtc = table.Column<DateTime>(nullable: false),
                    UpdateUtc = table.Column<DateTime>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    TransactionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_TransactionId",
                table: "Notes",
                column: "TransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropColumn(
                name: "InEscrow",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "TransactionItems");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TransactionItems");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Deals");
        }
    }
}
