using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CCMS.Migrations
{
    public partial class RemoveTimeFromHireDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "HireDate",
                table: "Employees",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "HireDate",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
