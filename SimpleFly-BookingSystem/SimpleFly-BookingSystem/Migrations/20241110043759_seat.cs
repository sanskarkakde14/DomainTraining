using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleFly_BookingSystem.Migrations
{
    public partial class seat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeatCount",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeatCount",
                table: "Bookings");
        }
    }
}
