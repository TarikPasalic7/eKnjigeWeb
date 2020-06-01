using Microsoft.EntityFrameworkCore.Migrations;

namespace eKnjige.Migrations
{
    public partial class migi4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mp3Path",
                table: "EKnjige",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PdfPath",
                table: "EKnjige",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mp3Path",
                table: "EKnjige");

            migrationBuilder.DropColumn(
                name: "PdfPath",
                table: "EKnjige");
        }
    }
}
