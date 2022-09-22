using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDnder.Data.Migrations
{
    public partial class UpdateToCampaignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "worldName",
                table: "Campaign",
                newName: "WorldName");

            migrationBuilder.RenameColumn(
                name: "details",
                table: "Campaign",
                newName: "Details");

            migrationBuilder.RenameColumn(
                name: "campaignName",
                table: "Campaign",
                newName: "CampaignName");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Campaign",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorldName",
                table: "Campaign",
                newName: "worldName");

            migrationBuilder.RenameColumn(
                name: "Details",
                table: "Campaign",
                newName: "details");

            migrationBuilder.RenameColumn(
                name: "CampaignName",
                table: "Campaign",
                newName: "campaignName");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Campaign",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
