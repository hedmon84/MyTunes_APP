using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTunes_app.api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "albmus",
                columns: table => new
                {
                    idalbum = table.Column<decimal>(type: "TEXT", nullable: false),
                    album_name = table.Column<string>(type: "TEXT", nullable: false),
                    artist_name = table.Column<string>(type: "TEXT", nullable: false),
                    price = table.Column<decimal>(type: "TEXT", nullable: false),
                    style = table.Column<string>(type: "TEXT", nullable: false),
                    rating = table.Column<decimal>(type: "TEXT", nullable: false),
                    date_departure = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    img = table.Column<string>(type: "TEXT", nullable: false),
                    buyalbum = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_albmus", x => x.idalbum);
                });

            migrationBuilder.CreateTable(
                name: "songs",
                columns: table => new
                {
                    idsong = table.Column<double>(type: "REAL", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    artist = table.Column<string>(type: "TEXT", nullable: false),
                    price = table.Column<double>(type: "REAL", nullable: false),
                    duration = table.Column<double>(type: "REAL", nullable: false),
                    rating = table.Column<double>(type: "REAL", nullable: false),
                    buyclick = table.Column<bool>(type: "INTEGER", nullable: false),
                    Albumidalbum = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_songs", x => x.idsong);
                    table.ForeignKey(
                        name: "FK_songs_albmus_Albumidalbum",
                        column: x => x.Albumidalbum,
                        principalTable: "albmus",
                        principalColumn: "idalbum",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_songs_Albumidalbum",
                table: "songs",
                column: "Albumidalbum");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "songs");

            migrationBuilder.DropTable(
                name: "albmus");
        }
    }
}
