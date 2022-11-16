using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDnder.Data.Migrations
{
    public partial class Added_DM_Chat_Functionality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_CampaignListing_CampaignListingId",
                table: "Character");

            migrationBuilder.DropIndex(
                name: "IX_Character_CampaignListingId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "CampaignListingId",
                table: "Character");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CampaignListingId",
                table: "Character",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Character_CampaignListingId",
                table: "Character",
                column: "CampaignListingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_CampaignListing_CampaignListingId",
                table: "Character",
                column: "CampaignListingId",
                principalTable: "CampaignListing",
                principalColumn: "Id");
        }
    }
}
