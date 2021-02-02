using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CalisiyorMu.Migrations
{
    public partial class AddedEndtime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                "EndTime",
                "Studies",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }



        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "EndTime",
                "Studies");
        }
    }
}