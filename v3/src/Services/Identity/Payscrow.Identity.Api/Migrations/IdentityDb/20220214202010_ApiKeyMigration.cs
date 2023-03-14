using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.Identity.Api.Migrations.IdentityDb
{
    public partial class ApiKeyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiKeys",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    CreateUserId = table.Column<Guid>(nullable: false),
                    CreateUtc = table.Column<DateTime>(nullable: false),
                    UpdateUserId = table.Column<Guid>(nullable: false),
                    UpdateUtc = table.Column<DateTime>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Label = table.Column<string>(maxLength: 200, nullable: true),
                    Key = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiKeys_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Hosts",
                columns: new[] { "Id", "Name", "TenantId" },
                values: new object[] { new Guid("009602d0-7b36-404e-9496-e5946a3f2633"), "host.docker.internal:8000", new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("30867e39-acca-4565-b5e5-c3785b6f8899"),
                columns: new[] { "CreateUtc", "UpdateUtc" },
                values: new object[] { new DateTime(2022, 2, 14, 20, 20, 10, 105, DateTimeKind.Utc).AddTicks(1318), new DateTime(2022, 2, 14, 20, 20, 10, 105, DateTimeKind.Utc).AddTicks(1318) });

            migrationBuilder.CreateIndex(
                name: "IX_ApiKeys_AccountId",
                table: "ApiKeys",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiKeys");

            migrationBuilder.DeleteData(
                table: "Hosts",
                keyColumn: "Id",
                keyValue: new Guid("009602d0-7b36-404e-9496-e5946a3f2633"));

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("30867e39-acca-4565-b5e5-c3785b6f8899"),
                columns: new[] { "CreateUtc", "UpdateUtc" },
                values: new object[] { new DateTime(2021, 11, 20, 5, 22, 39, 10, DateTimeKind.Utc).AddTicks(8127), new DateTime(2021, 11, 20, 5, 22, 39, 10, DateTimeKind.Utc).AddTicks(8127) });
        }
    }
}
