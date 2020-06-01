using Microsoft.EntityFrameworkCore.Migrations;

namespace eKnjige.Migrations
{
    public partial class PrijedlogIzmjena : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Odgovoren",
                table: "PrijedlogKnjiga",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Prihvacen",
                table: "PrijedlogKnjiga",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Odgovoren",
                table: "PrijedlogKnjiga");

            migrationBuilder.DropColumn(
                name: "Prihvacen",
                table: "PrijedlogKnjiga");
        }
    }
}
