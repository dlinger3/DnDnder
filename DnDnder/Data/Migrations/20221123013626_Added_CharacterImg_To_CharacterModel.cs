using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDnder.Data.Migrations
{
    public partial class Added_CharacterImg_To_CharacterModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CharacterImg",
                table: "Character",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CharacterImg",
                table: "Character");
        }
    }
}
