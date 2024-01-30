using Microsoft.EntityFrameworkCore.Migrations;

namespace ReversiMVCProper.Migrations
{
    public partial class addspel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Spel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Omschrijving = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AanDeBeurt = table.Column<int>(type: "int", nullable: false),
                    Speler1Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Speler2Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bord = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Spel");
        }
    }
}
