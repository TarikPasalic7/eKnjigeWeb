using Microsoft.EntityFrameworkCore.Migrations;

namespace eKnjige.Migrations
{
    public partial class mig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KomentarKlijenti");

            migrationBuilder.AddColumn<int>(
                name: "EKnjigaID",
                table: "Komentari",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KlijentID",
                table: "Komentari",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Komentari_EKnjigaID",
                table: "Komentari",
                column: "EKnjigaID");

            migrationBuilder.CreateIndex(
                name: "IX_Komentari_KlijentID",
                table: "Komentari",
                column: "KlijentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Komentari_EKnjige_EKnjigaID",
                table: "Komentari",
                column: "EKnjigaID",
                principalTable: "EKnjige",
                principalColumn: "EKnjigaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Komentari_Klijenti_KlijentID",
                table: "Komentari",
                column: "KlijentID",
                principalTable: "Klijenti",
                principalColumn: "KlijentID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Komentari_EKnjige_EKnjigaID",
                table: "Komentari");

            migrationBuilder.DropForeignKey(
                name: "FK_Komentari_Klijenti_KlijentID",
                table: "Komentari");

            migrationBuilder.DropIndex(
                name: "IX_Komentari_EKnjigaID",
                table: "Komentari");

            migrationBuilder.DropIndex(
                name: "IX_Komentari_KlijentID",
                table: "Komentari");

            migrationBuilder.DropColumn(
                name: "EKnjigaID",
                table: "Komentari");

            migrationBuilder.DropColumn(
                name: "KlijentID",
                table: "Komentari");

            migrationBuilder.CreateTable(
                name: "KomentarKlijenti",
                columns: table => new
                {
                    KomentarKlijentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EKnjigaID = table.Column<int>(type: "int", nullable: false),
                    KlijentID = table.Column<int>(type: "int", nullable: false),
                    KomentarID = table.Column<int>(type: "int", nullable: false)
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
                        onDelete: ReferentialAction.Cascade);
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
    }
}
