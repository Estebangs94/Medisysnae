using Microsoft.EntityFrameworkCore.Migrations;

namespace Medisysnae.Migrations
{
    public partial class MostrarPass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MostrarPass",
                table: "Profesional",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MostrarPass",
                table: "Profesional");
        }
    }
}
