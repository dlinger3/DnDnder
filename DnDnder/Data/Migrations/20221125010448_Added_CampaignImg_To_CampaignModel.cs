using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDnder.Data.Migrations
{
    public partial class Added_CampaignImg_To_CampaignModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CampaignImg",
                table: "Campaign",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CampaignImg",
                table: "Campaign");
        }
    }
}
