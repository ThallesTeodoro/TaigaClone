using Microsoft.EntityFrameworkCore.Migrations;

namespace Taiga.Infrastructure.Migrations
{
    public partial class AddedTypeToEmailConfirmationCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "email_confirmation_codes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "email_confirmation_codes");
        }
    }
}
