using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDnder.Data.Migrations
{
    public partial class InitialCreateAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Character",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserID",
                table: "Campaign",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Character_AppUserId",
                table: "Character",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_AppUserID",
                table: "Campaign",
                column: "AppUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaign_AspNetUsers_AppUserID",
                table: "Campaign",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Character_AspNetUsers_AppUserId",
                table: "Character",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaign_AspNetUsers_AppUserID",
                table: "Campaign");

            migrationBuilder.DropForeignKey(
                name: "FK_Character_AspNetUsers_AppUserId",
                table: "Character");

            migrationBuilder.DropIndex(
                name: "IX_Character_AppUserId",
                table: "Character");

            migrationBuilder.DropIndex(
                name: "IX_Campaign_AppUserID",
                table: "Campaign");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "Campaign");
        }
    }
}
