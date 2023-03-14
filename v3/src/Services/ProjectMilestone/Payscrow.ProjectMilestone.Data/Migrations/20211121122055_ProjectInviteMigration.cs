using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.ProjectMilestone.Data.Migrations
{
    public partial class ProjectInviteMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Projects",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "ReceiverAccountName",
                table: "Projects",
                newName: "PayeeAccountName");

            migrationBuilder.RenameColumn(
                name: "PaymentReceiverAccountId",
                table: "Projects",
                newName: "PayeeAccountId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Milestones",
                newName: "StatusId");

            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Projects",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Invites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartnerEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ResponseLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SentEmailCount = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invites_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "AccountId", "Code", "CreateUserId", "CreateUtc", "IsActive", "Name", "Order", "PercentageCharge", "Symbol", "TenantId", "UpdateUserId", "UpdateUtc" },
                values: new object[] { new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"), new Guid("00000000-0000-0000-0000-000000000000"), "NGN", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Naira", 1, 0m, "N", new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTime(2020, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "AccountId", "Code", "CreateUserId", "CreateUtc", "IsActive", "Name", "Order", "PercentageCharge", "Symbol", "TenantId", "UpdateUserId", "UpdateUtc" },
                values: new object[] { new Guid("1c9695e0-330b-4b5b-a866-edd0efe674fa"), new Guid("00000000-0000-0000-0000-000000000000"), "USD", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "US Dollar", 2, 0m, "$", new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTime(2020, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CurrencyId",
                table: "Projects",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invites_ProjectId",
                table: "Invites",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Currencies_CurrencyId",
                table: "Projects",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Currencies_CurrencyId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "Invites");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CurrencyId",
                table: "Projects");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("1c9695e0-330b-4b5b-a866-edd0efe674fa"));

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Projects",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "PayeeAccountName",
                table: "Projects",
                newName: "ReceiverAccountName");

            migrationBuilder.RenameColumn(
                name: "PayeeAccountId",
                table: "Projects",
                newName: "PaymentReceiverAccountId");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Milestones",
                newName: "Status");
        }
    }
}
