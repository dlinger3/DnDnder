using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDnder.Data.Migrations
{
    public partial class Created_Tables_For_ChatGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SentBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListingChapGroupID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_ListingChatGroups_ListingChapGroupID",
                        column: x => x.ListingChapGroupID,
                        principalTable: "ListingChatGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListingChatGroups_CampaignListingID",
                table: "ListingChatGroups",
                column: "CampaignListingID");

            migrationBuilder.CreateIndex(
                name: "IX_ListingChatGroups_MessageID",
                table: "ListingChatGroups",
                column: "MessageID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ListingChapGroupID",
                table: "Messages",
                column: "ListingChapGroupID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ListingChatGroups_Messages_MessageID",
                table: "ListingChatGroups",
                column: "MessageID",
                principalTable: "Messages",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListingChatGroups_Messages_MessageID",
                table: "ListingChatGroups");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "ListingChatGroups");
        }
    }
}
