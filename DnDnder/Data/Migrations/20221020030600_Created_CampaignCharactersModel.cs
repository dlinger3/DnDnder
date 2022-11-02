using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDnder.Data.Migrations
{
    public partial class Created_CampaignCharactersModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CampaignCharacters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaignListingID = table.Column<int>(type: "int", nullable: true),
                    CharacterID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignCharacters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampaignCharacters_CampaignListing_CampaignListingID",
                        column: x => x.CampaignListingID,
                        principalTable: "CampaignListing",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CampaignCharacters_Character_CharacterID",
                        column: x => x.CharacterID,
                        principalTable: "Character",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampaignCharacters_CampaignListingID",
                table: "CampaignCharacters",
                column: "CampaignListingID");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignCharacters_CharacterID",
                table: "CampaignCharacters",
                column: "CharacterID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampaignCharacters");
        }
    }
}
