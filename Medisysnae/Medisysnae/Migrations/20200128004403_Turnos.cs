using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Medisysnae.Migrations
{
    public partial class Turnos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodMedico",
                table: "Turno");

            migrationBuilder.DropColumn(
                name: "FechaHoraCancelacion",
                table: "Turno");

            migrationBuilder.RenameColumn(
                name: "CodPaciente",
                table: "Turno",
                newName: "Paciente_ID");

            migrationBuilder.AddColumn<string>(
                name: "Comentario",
                table: "Turno",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LugarAtencion",
                table: "Turno",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreUsuario",
                table: "Turno",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Turno_Paciente_ID",
                table: "Turno",
                column: "Paciente_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Turno_Paciente_Paciente_ID",
                table: "Turno",
                column: "Paciente_ID",
                principalTable: "Paciente",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turno_Paciente_Paciente_ID",
                table: "Turno");

            migrationBuilder.DropIndex(
                name: "IX_Turno_Paciente_ID",
                table: "Turno");

            migrationBuilder.DropColumn(
                name: "Comentario",
                table: "Turno");

            migrationBuilder.DropColumn(
                name: "LugarAtencion",
                table: "Turno");

            migrationBuilder.DropColumn(
                name: "NombreUsuario",
                table: "Turno");

            migrationBuilder.RenameColumn(
                name: "Paciente_ID",
                table: "Turno",
                newName: "CodPaciente");

            migrationBuilder.AddColumn<int>(
                name: "CodMedico",
                table: "Turno",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaHoraCancelacion",
                table: "Turno",
                nullable: true);
        }
    }
}
