using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.Notifications.Api.Data.Migrations
{
    public partial class EmailProviderMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "EmailLogs");

            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "EmailLogs");

            migrationBuilder.CreateTable(
                name: "EmailProviders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    CreateUtc = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    ProviderApiKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailProviders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    CreateUtc = table.Column<DateTime>(nullable: false),
                    MessageType = table.Column<int>(nullable: false),
                    ProviderTemplateId = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    EmailProviderId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailTemplates_EmailProviders_EmailProviderId",
                        column: x => x.EmailProviderId,
                        principalTable: "EmailProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmailTemplates_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EmailProviders",
                columns: new[] { "Id", "CreateUtc", "ProviderApiKey", "TenantId", "Type" },
                values: new object[] { new Guid("80ffa783-157d-46cf-9905-ccb22a72d937"), new DateTime(2022, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "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", new Guid("30867e39-acca-4565-b5e5-c3785b6f8899"), 0 });

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("30867e39-acca-4565-b5e5-c3785b6f8899"),
                column: "Name",
                value: "PayScrow");

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "Content", "CreateUtc", "EmailProviderId", "MessageType", "ProviderTemplateId", "TenantId" },
                values: new object[,]
                {
                    { new Guid("efd4e7ef-fd43-4161-8943-786f94b42d36"), "", new DateTime(2022, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("80ffa783-157d-46cf-9905-ccb22a72d937"), 60, "d-61c8257fa427411cb5bf71777201b14b", new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") },
                    { new Guid("259e5fe4-5d76-44b7-9dda-2945ceef7613"), "", new DateTime(2022, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("80ffa783-157d-46cf-9905-ccb22a72d937"), 70, "d-19e87ca99cff4c7f96203e2a8a41cc39", new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") },
                    { new Guid("0791209d-2815-407d-a20a-4cb0ecb7051b"), "", new DateTime(2022, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("80ffa783-157d-46cf-9905-ccb22a72d937"), 200, "d-909fb1fdd5ad48f4b42175208e38cd8b", new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") },
                    { new Guid("87b8343e-9c1d-4ced-b777-255cd5da1c8b"), "", new DateTime(2022, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("80ffa783-157d-46cf-9905-ccb22a72d937"), 210, "d-3ff07847949d4a5eb00a349fd0861598", new Guid("30867e39-acca-4565-b5e5-c3785b6f8899") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_EmailProviderId",
                table: "EmailTemplates",
                column: "EmailProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_TenantId",
                table: "EmailTemplates",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailTemplates");

            migrationBuilder.DropTable(
                name: "EmailProviders");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "EmailLogs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateUserId",
                table: "EmailLogs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("30867e39-acca-4565-b5e5-c3785b6f8899"),
                column: "Name",
                value: "Payscrow");
        }
    }
}