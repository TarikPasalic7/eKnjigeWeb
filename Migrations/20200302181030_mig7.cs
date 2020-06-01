using Microsoft.EntityFrameworkCore.Migrations;

namespace eKnjige.Migrations
{
    public partial class mig7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ocijena",
                table: "KlijentKnjigaOcijene");

            migrationBuilder.DropColumn(
                name: "OcijenaKnjige",
                table: "EKnjige");

            migrationBuilder.AddColumn<float>(
                name: "Ocjena",
                table: "KlijentKnjigaOcijene",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "OcjenaKnjige",
                table: "EKnjige",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ocjena",
                table: "KlijentKnjigaOcijene");

            migrationBuilder.DropColumn(
                name: "OcjenaKnjige",
                table: "EKnjige");

            migrationBuilder.AddColumn<float>(
                name: "Ocijena",
                table: "KlijentKnjigaOcijene",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "OcijenaKnjige",
                table: "EKnjige",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
