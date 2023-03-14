using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.PaymentInvite.Data.Migrations
{
    public partial class TransactionStatusLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreateUserId",
                table: "Transactions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "Transactions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreateUserId",
                table: "TransactionItems",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "TransactionItems",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreateUserId",
                table: "Traders",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "Traders",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreateUserId",
                table: "Notes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "Notes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreateUserId",
                table: "Deals",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "Deals",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreateUserId",
                table: "DealItems",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "DealItems",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreateUserId",
                table: "Currencies",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "Currencies",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "TransactionStatusLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateUtc = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<Guid>(nullable: false),
                    UpdateUtc = table.Column<DateTime>(nullable: false),
                    UpdateUserId = table.Column<Guid>(nullable: false),
                    TransactionStatusId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    TransactionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionStatusLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionStatusLogs_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionStatusLogs_TransactionId",
                table: "TransactionStatusLogs",
                column: "TransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionStatusLogs");

            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "TransactionItems");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "TransactionItems");

            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "Traders");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "Traders");

            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "DealItems");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "DealItems");

            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "Currencies");
        }
    }
}
