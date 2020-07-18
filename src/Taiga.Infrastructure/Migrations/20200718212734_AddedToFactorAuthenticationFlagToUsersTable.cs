using Microsoft.EntityFrameworkCore.Migrations;

namespace Taiga.Infrastructure.Migrations
{
    public partial class AddedToFactorAuthenticationFlagToUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TowFactorAuthentication",
                table: "users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TowFactorAuthentication",
                table: "users");
        }
    }
}
