using Microsoft.EntityFrameworkCore.Migrations;

namespace Medisysnae.Migrations
{
    public partial class DatosProfesional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DNI",
                table: "Profesional",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Profesional",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DNI",
                table: "Profesional");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Profesional");
        }
    }
}
