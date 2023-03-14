using Microsoft.EntityFrameworkCore.Migrations;

namespace Payscrow.Payments.Api.Data.Migrations
{
    public partial class PaymentMethodProvider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RedirectUrl",
                table: "PaymentMethods");

            migrationBuilder.AddColumn<int>(
                name: "Provider",
                table: "PaymentMethods",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Provider",
                table: "PaymentMethods");

            migrationBuilder.AddColumn<string>(
                name: "RedirectUrl",
                table: "PaymentMethods",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
