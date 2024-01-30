using Microsoft.EntityFrameworkCore.Migrations;

namespace ReversiMVCProper.Migrations
{
    public partial class add_role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Spel",
                newName: "ID");

            migrationBuilder.AddColumn<int>(
                name: "Roles",
                table: "Spelers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Winnaar",
                table: "Spel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Roles",
                table: "Spelers");

            migrationBuilder.DropColumn(
                name: "Winnaar",
                table: "Spel");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Spel",
                newName: "Id");
        }
    }
}
