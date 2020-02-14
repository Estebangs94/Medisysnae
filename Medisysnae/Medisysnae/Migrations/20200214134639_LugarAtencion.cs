using Microsoft.EntityFrameworkCore.Migrations;

namespace Medisysnae.Migrations
{
    public partial class LugarAtencion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfesionalID",
                table: "LugaresAtencion");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioProfesional",
                table: "LugaresAtencion",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioProfesional",
                table: "LugaresAtencion");

            migrationBuilder.AddColumn<int>(
                name: "ProfesionalID",
                table: "LugaresAtencion",
                nullable: false,
                defaultValue: 0);
        }
    }
}
