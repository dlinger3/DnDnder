using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDnder.Data.Migrations
{
    public partial class Updated_Message_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SocketId",
                table: "Message");

            migrationBuilder.AddColumn<int>(
                name: "CampaignListingID",
                table: "Message",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Message_CampaignListingID",
                table: "Message",
                column: "CampaignListingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_CampaignListing_CampaignListingID",
                table: "Message",
                column: "CampaignListingID",
                principalTable: "CampaignListing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_CampaignListing_CampaignListingID",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_CampaignListingID",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "CampaignListingID",
                table: "Message");

            migrationBuilder.AddColumn<string>(
                name: "SocketId",
                table: "Message",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
