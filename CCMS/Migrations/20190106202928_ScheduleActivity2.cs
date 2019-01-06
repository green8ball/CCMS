using Microsoft.EntityFrameworkCore.Migrations;

namespace CCMS.Migrations
{
    public partial class ScheduleActivity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ScheduleActivityCodes",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Priority",
                table: "ScheduleActivityCodes",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ScheduleActivityCodes");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "ScheduleActivityCodes");
        }
    }
}
