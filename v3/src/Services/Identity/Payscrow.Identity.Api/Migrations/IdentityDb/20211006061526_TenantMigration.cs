using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.Identity.Api.Migrations.IdentityDb
{
    public partial class TenantMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "ApplicationUserAccounts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Accounts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true),
                    ContactPhone = table.Column<string>(nullable: true),
                    ContactEmail = table.Column<string>(nullable: true),
                    DomainName = table.Column<string>(nullable: true),
                    Colour = table.Column<string>(nullable: true),
                    LogoUrl = table.Column<string>(nullable: true),
                    CreateUserId = table.Column<Guid>(nullable: false),
                    CreateUtc = table.Column<DateTime>(nullable: false),
                    UpdateUserId = table.Column<Guid>(nullable: false),
                    UpdateUtc = table.Column<DateTime>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ApplicationUserAccounts");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Accounts");
        }
    }
}
