using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDnder.Data.Migrations
{
    public partial class Final_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListingChatGroups");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListingChatGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaignListingID = table.Column<int>(type: "int", nullable: false),
                    MessageID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingChatGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListingChatGroups_CampaignListing_CampaignListingID",
                        column: x => x.CampaignListingID,
                        principalTable: "CampaignListing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListingChatGroups_Message_MessageID",
                        column: x => x.MessageID,
                        principalTable: "Message",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListingChatGroups_CampaignListingID",
                table: "ListingChatGroups",
                column: "CampaignListingID");

            migrationBuilder.CreateIndex(
                name: "IX_ListingChatGroups_MessageID",
                table: "ListingChatGroups",
                column: "MessageID");
        }
    }
}
