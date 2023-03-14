using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.Escrow.Data.Migrations
{
    public partial class ApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoFileName",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Country",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_State",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_ZipCode",
                table: "Accounts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateUtc = table.Column<DateTime>(nullable: false),
                    UpdateUtc = table.Column<DateTime>(nullable: false),
                    UserGuid = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    MiddleName = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: true),
                    AvatarFileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserGuid",
                table: "Users",
                column: "UserGuid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropColumn(
                name: "LogoFileName",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Address_Country",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Address_State",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Address_ZipCode",
                table: "Accounts");
        }
    }
}
