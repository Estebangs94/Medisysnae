using Microsoft.EntityFrameworkCore.Migrations;

namespace Medisysnae.Migrations
{
    public partial class FechasString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FechaCreacionString",
                table: "Paciente",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FechaHoraString",
                table: "Atencion",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacionString",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "FechaHoraString",
                table: "Atencion");
        }
    }
}
