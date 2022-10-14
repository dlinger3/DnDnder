using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDnder.Data.Migrations
{
    public partial class FKUserIDAddedToCampaignListing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserID",
                table: "CampaignListing",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CampaignListing_AppUserID",
                table: "CampaignListing",
                column: "AppUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignListing_AspNetUsers_AppUserID",
                table: "CampaignListing",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampaignListing_AspNetUsers_AppUserID",
                table: "CampaignListing");

            migrationBuilder.DropIndex(
                name: "IX_CampaignListing_AppUserID",
                table: "CampaignListing");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "CampaignListing");
        }
    }
}
