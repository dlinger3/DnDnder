using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDnder.Data.Migrations
{
    public partial class Updated_Fields_For_MessageModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListingChatGroups_Messages_MessageID",
                table: "ListingChatGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_ListingChatGroups_ListingChapGroupID",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ListingChapGroupID",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ListingChapGroupID",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "Message");

            migrationBuilder.AddColumn<string>(
                name: "SocketId",
                table: "Message",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ListingChatGroups_Message_MessageID",
                table: "ListingChatGroups",
                column: "MessageID",
                principalTable: "Message",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListingChatGroups_Message_MessageID",
                table: "ListingChatGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "SocketId",
                table: "Message");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "ListingChapGroupID",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_ListingChatGroups_ListingChapGroupID",
                table: "Messages",
                column: "ListingChapGroupID",
                principalTable: "ListingChatGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
