using Microsoft.EntityFrameworkCore.Migrations;

namespace CalisiyorMu.Migrations.Auth
{
    public partial class AddAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "NickName",
                "AspNetUsers",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }



        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "NickName",
                "AspNetUsers");
        }
    }
}