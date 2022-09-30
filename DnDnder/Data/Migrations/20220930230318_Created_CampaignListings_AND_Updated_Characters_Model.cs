using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDnder.Data.Migrations
{
    public partial class Created_CampaignListings_AND_Updated_Characters_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaign_AspNetUsers_AppUserID",
                table: "Campaign");

            migrationBuilder.DropForeignKey(
                name: "FK_Character_AspNetUsers_AppUserId",
                table: "Character");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Character",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "level",
                table: "Character",
                newName: "Level");

            migrationBuilder.RenameColumn(
                name: "bio",
                table: "Character",
                newName: "Bio");

            migrationBuilder.RenameColumn(
                name: "alignment",
                table: "Character",
                newName: "Alignment");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Character",
                newName: "AppUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Character_AppUserId",
                table: "Character",
                newName: "IX_Character_AppUserID");

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

            migrationBuilder.AddColumn<int>(
                name: "CampaignListingId",
                table: "Character",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AppUserID",
                table: "Campaign",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "CampaignListing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaignId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignListing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampaignListing_Campaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaign",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Character_CampaignListingId",
                table: "Character",
                column: "CampaignListingId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignListing_CampaignId",
                table: "CampaignListing",
                column: "CampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaign_AspNetUsers_AppUserID",
                table: "Campaign",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_AspNetUsers_AppUserID",
                table: "Character",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_CampaignListing_CampaignListingId",
                table: "Character",
                column: "CampaignListingId",
                principalTable: "CampaignListing",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaign_AspNetUsers_AppUserID",
                table: "Campaign");

            migrationBuilder.DropForeignKey(
                name: "FK_Character_AspNetUsers_AppUserID",
                table: "Character");

            migrationBuilder.DropForeignKey(
                name: "FK_Character_CampaignListing_CampaignListingId",
                table: "Character");

            migrationBuilder.DropTable(
                name: "CampaignListing");

            migrationBuilder.DropIndex(
                name: "IX_Character_CampaignListingId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "CampaignListingId",
                table: "Character");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Character",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "Character",
                newName: "level");

            migrationBuilder.RenameColumn(
                name: "Bio",
                table: "Character",
                newName: "bio");

            migrationBuilder.RenameColumn(
                name: "AppUserID",
                table: "Character",
                newName: "AppUserId");

            migrationBuilder.RenameColumn(
                name: "Alignment",
                table: "Character",
                newName: "alignment");

            migrationBuilder.RenameIndex(
                name: "IX_Character_AppUserID",
                table: "Character",
                newName: "IX_Character_AppUserId");

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

            migrationBuilder.AlterColumn<string>(
                name: "AppUserID",
                table: "Campaign",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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
    }
}
