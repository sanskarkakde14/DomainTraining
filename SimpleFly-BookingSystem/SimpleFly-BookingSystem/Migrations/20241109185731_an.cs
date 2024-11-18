using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleFly_BookingSystem.Migrations
{
    public partial class an : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordSalt",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
