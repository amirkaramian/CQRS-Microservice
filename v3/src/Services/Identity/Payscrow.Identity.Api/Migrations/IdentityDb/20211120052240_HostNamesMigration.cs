using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.Identity.Api.Migrations.IdentityDb
{
    public partial class HostNamesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateUtc",
                table: "Tenants",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdateUserId",
                table: "Tenants",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "Hosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hosts_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Hosts",
                columns: new[] { "Id", "Name", "TenantId" },
                values: new object[] { new Guid("81ae491e-7c4e-4f8d-93cb-adc8016355c2"), "host.docker.internal:7100", new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") });

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("30867e39-acca-4565-b5e5-c3785b6f8899"),
                columns: new[] { "CreateUtc", "DomainName", "UpdateUserId", "UpdateUtc" },
                values: new object[] { new DateTime(2021, 11, 20, 5, 22, 39, 10, DateTimeKind.Utc).AddTicks(8127), null, null, new DateTime(2021, 11, 20, 5, 22, 39, 10, DateTimeKind.Utc).AddTicks(8127) });

            migrationBuilder.CreateIndex(
                name: "IX_Hosts_Name",
                table: "Hosts",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Hosts_TenantId",
                table: "Hosts",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hosts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateUtc",
                table: "Tenants",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdateUserId",
                table: "Tenants",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("30867e39-acca-4565-b5e5-c3785b6f8899"),
                columns: new[] { "CreateUtc", "DomainName", "UpdateUserId", "UpdateUtc" },
                values: new object[] { new DateTime(2021, 11, 19, 1, 17, 3, 252, DateTimeKind.Utc).AddTicks(7947), "host.docker.internal", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2021, 11, 19, 1, 17, 3, 252, DateTimeKind.Utc).AddTicks(7947) });
        }
    }
}
