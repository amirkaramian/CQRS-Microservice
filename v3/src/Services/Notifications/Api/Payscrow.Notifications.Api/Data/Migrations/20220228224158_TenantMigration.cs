using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.Notifications.Api.Data.Migrations
{
    public partial class TenantMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    CreateUserId = table.Column<Guid>(nullable: true),
                    CreateUtc = table.Column<DateTime>(nullable: false),
                    To = table.Column<string>(nullable: true),
                    From = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    IsSent = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "EmailAddress", "Name" },
                values: new object[] { new Guid("30867e39-acca-4565-b5e5-c3785b6f8899"), "hello@payscrow.net", "Payscrow" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailLogs");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
