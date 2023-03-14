using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.Identity.Api.Migrations.IdentityDb
{
    public partial class SeedTenantsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Colour", "CompanyName", "ContactEmail", "ContactPhone", "CreateUserId", "CreateUtc", "DomainName", "LogoUrl", "UpdateUserId", "UpdateUtc" },
                values: new object[] { new Guid("30867e39-acca-4565-b5e5-c3785b6f8899"), "Green", "Payscrow", "hello@payscrow.net", "08037452476", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2021, 11, 19, 1, 17, 3, 252, DateTimeKind.Utc).AddTicks(7947), "host.docker.internal", null, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2021, 11, 19, 1, 17, 3, 252, DateTimeKind.Utc).AddTicks(7947) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("30867e39-acca-4565-b5e5-c3785b6f8899"));
        }
    }
}
