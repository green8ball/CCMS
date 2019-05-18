using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CCMS.Migrations
{
    public partial class SchedulePattern : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SchedulePatterns",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulePatterns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchedulePatternRows",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WeekNumber = table.Column<int>(nullable: false),
                    DOW = table.Column<byte>(nullable: false),
                    TimeIn = table.Column<DateTime>(nullable: false),
                    TimeOut = table.Column<DateTime>(nullable: false),
                    SchedulePatternId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulePatternRows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchedulePatternRows_SchedulePatterns_SchedulePatternId",
                        column: x => x.SchedulePatternId,
                        principalTable: "SchedulePatterns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchedulePatternRows_SchedulePatternId",
                table: "SchedulePatternRows",
                column: "SchedulePatternId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchedulePatternRows");

            migrationBuilder.DropTable(
                name: "SchedulePatterns");
        }
    }
}
