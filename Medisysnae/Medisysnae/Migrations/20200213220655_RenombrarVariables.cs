using Microsoft.EntityFrameworkCore.Migrations;

namespace Medisysnae.Migrations
{
    public partial class RenombrarVariables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstaHabilitado",
                table: "Profesional",
                newName: "EstaActivo");

            migrationBuilder.RenameColumn(
                name: "EstaEliminada",
                table: "Atencion",
                newName: "EstaActiva");

            migrationBuilder.AddColumn<bool>(
                name: "EstaActivo",
                table: "Turno",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstaActivo",
                table: "Turno");

            migrationBuilder.RenameColumn(
                name: "EstaActivo",
                table: "Profesional",
                newName: "EstaHabilitado");

            migrationBuilder.RenameColumn(
                name: "EstaActiva",
                table: "Atencion",
                newName: "EstaEliminada");
        }
    }
}
