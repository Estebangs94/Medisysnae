using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Medisysnae.Migrations
{
    public partial class ModifcacionesTurno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turno_Profesional_MedicoNombreUsuario",
                table: "Turno");

            migrationBuilder.DropForeignKey(
                name: "FK_Turno_Paciente_PacienteID",
                table: "Turno");

            migrationBuilder.DropForeignKey(
                name: "FK_Turno_Tratamiento_TratamientoID",
                table: "Turno");

            migrationBuilder.DropIndex(
                name: "IX_Turno_MedicoNombreUsuario",
                table: "Turno");

            migrationBuilder.DropIndex(
                name: "IX_Turno_PacienteID",
                table: "Turno");

            migrationBuilder.DropIndex(
                name: "IX_Turno_TratamientoID",
                table: "Turno");

            migrationBuilder.DropColumn(
                name: "MedicoNombreUsuario",
                table: "Turno");

            migrationBuilder.DropColumn(
                name: "PacienteID",
                table: "Turno");

            migrationBuilder.DropColumn(
                name: "TratamientoID",
                table: "Turno");

            migrationBuilder.RenameColumn(
                name: "FechaHora",
                table: "Turno",
                newName: "FechaTurno");

            migrationBuilder.AddColumn<int>(
                name: "CodMedico",
                table: "Turno",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CodPaciente",
                table: "Turno",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "HoraComienzo",
                table: "Turno",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HoraFin",
                table: "Turno",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DNI",
                table: "Profesional",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodMedico",
                table: "Turno");

            migrationBuilder.DropColumn(
                name: "CodPaciente",
                table: "Turno");

            migrationBuilder.DropColumn(
                name: "HoraComienzo",
                table: "Turno");

            migrationBuilder.DropColumn(
                name: "HoraFin",
                table: "Turno");

            migrationBuilder.RenameColumn(
                name: "FechaTurno",
                table: "Turno",
                newName: "FechaHora");

            migrationBuilder.AddColumn<string>(
                name: "MedicoNombreUsuario",
                table: "Turno",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PacienteID",
                table: "Turno",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TratamientoID",
                table: "Turno",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DNI",
                table: "Profesional",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Turno_MedicoNombreUsuario",
                table: "Turno",
                column: "MedicoNombreUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Turno_PacienteID",
                table: "Turno",
                column: "PacienteID");

            migrationBuilder.CreateIndex(
                name: "IX_Turno_TratamientoID",
                table: "Turno",
                column: "TratamientoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Turno_Profesional_MedicoNombreUsuario",
                table: "Turno",
                column: "MedicoNombreUsuario",
                principalTable: "Profesional",
                principalColumn: "NombreUsuario",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Turno_Paciente_PacienteID",
                table: "Turno",
                column: "PacienteID",
                principalTable: "Paciente",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Turno_Tratamiento_TratamientoID",
                table: "Turno",
                column: "TratamientoID",
                principalTable: "Tratamiento",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
