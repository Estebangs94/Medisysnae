using Microsoft.EntityFrameworkCore.Migrations;

namespace Medisysnae.Migrations
{
    public partial class DiagnosticoAtencion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Diagnostico",
                table: "Atencion",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Diagnostico",
                table: "Atencion");
        }
    }
}
