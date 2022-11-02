using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDnder.Data.Migrations
{
    public partial class Added_AppUserFK_ToCampaignCharacters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserID",
                table: "CampaignCharacters",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CampaignCharacters_AppUserID",
                table: "CampaignCharacters",
                column: "AppUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignCharacters_AspNetUsers_AppUserID",
                table: "CampaignCharacters",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampaignCharacters_AspNetUsers_AppUserID",
                table: "CampaignCharacters");

            migrationBuilder.DropIndex(
                name: "IX_CampaignCharacters_AppUserID",
                table: "CampaignCharacters");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "CampaignCharacters");
        }
    }
}
