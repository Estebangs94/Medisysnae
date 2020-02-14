using Microsoft.EntityFrameworkCore.Migrations;

namespace Medisysnae.Migrations
{
    public partial class TurnoPacNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turno_Paciente_Paciente_ID",
                table: "Turno");

            migrationBuilder.AlterColumn<int>(
                name: "Paciente_ID",
                table: "Turno",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Turno_Paciente_Paciente_ID",
                table: "Turno",
                column: "Paciente_ID",
                principalTable: "Paciente",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turno_Paciente_Paciente_ID",
                table: "Turno");

            migrationBuilder.AlterColumn<int>(
                name: "Paciente_ID",
                table: "Turno",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Turno_Paciente_Paciente_ID",
                table: "Turno",
                column: "Paciente_ID",
                principalTable: "Paciente",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
