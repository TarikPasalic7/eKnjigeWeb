using Microsoft.EntityFrameworkCore.Migrations;

namespace eKnjige.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KreditneKartice");

            migrationBuilder.CreateTable(
                name: "Komentari",
                columns: table => new
                {
                    KomentarId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    komentar = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komentari", x => x.KomentarId);
                });

            migrationBuilder.CreateTable(
                name: "KomentarKlijenti",
                columns: table => new
                {
                    KomentarKlijentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KlijentID = table.Column<int>(nullable: false),
                    KomentarID = table.Column<int>(nullable: false),
                    EKnjigaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KomentarKlijenti", x => x.KomentarKlijentID);
                    table.ForeignKey(
                        name: "FK_KomentarKlijenti_EKnjige_EKnjigaID",
                        column: x => x.EKnjigaID,
                        principalTable: "EKnjige",
                        principalColumn: "EKnjigaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KomentarKlijenti_Klijenti_KlijentID",
                        column: x => x.KlijentID,
                        principalTable: "Klijenti",
                        principalColumn: "KlijentID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KomentarKlijenti_Komentari_KomentarID",
                        column: x => x.KomentarID,
                        principalTable: "Komentari",
                        principalColumn: "KomentarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KomentarKlijenti_EKnjigaID",
                table: "KomentarKlijenti",
                column: "EKnjigaID");

            migrationBuilder.CreateIndex(
                name: "IX_KomentarKlijenti_KlijentID",
                table: "KomentarKlijenti",
                column: "KlijentID");

            migrationBuilder.CreateIndex(
                name: "IX_KomentarKlijenti_KomentarID",
                table: "KomentarKlijenti",
                column: "KomentarID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KomentarKlijenti");

            migrationBuilder.DropTable(
                name: "Komentari");

            migrationBuilder.CreateTable(
                name: "KreditneKartice",
                columns: table => new
                {
                    KreditnaKarticaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojKartice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CVCBroj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumIstekaKartice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KlijentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KreditneKartice", x => x.KreditnaKarticaID);
                    table.ForeignKey(
                        name: "FK_KreditneKartice_Klijenti_KlijentID",
                        column: x => x.KlijentID,
                        principalTable: "Klijenti",
                        principalColumn: "KlijentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KreditneKartice_KlijentID",
                table: "KreditneKartice",
                column: "KlijentID");
        }
    }
}
