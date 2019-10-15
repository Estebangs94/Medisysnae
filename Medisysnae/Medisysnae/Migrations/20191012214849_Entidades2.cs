using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Medisysnae.Migrations
{
    public partial class Entidades2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Obrasocial",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obrasocial", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MedicoNombreUsuario = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Apellido = table.Column<string>(nullable: true),
                    Dni = table.Column<int>(nullable: false),
                    ObraSocialID = table.Column<int>(nullable: true),
                    Domicilio = table.Column<string>(nullable: true),
                    Telefono = table.Column<int>(nullable: false),
                    NroAfiliado = table.Column<string>(nullable: true),
                    Comentario = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    EstaActivo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paciente", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Paciente_Profesional_MedicoNombreUsuario",
                        column: x => x.MedicoNombreUsuario,
                        principalTable: "Profesional",
                        principalColumn: "NombreUsuario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Paciente_Obrasocial_ObraSocialID",
                        column: x => x.ObraSocialID,
                        principalTable: "Obrasocial",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Atencion",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MedicoNombreUsuario = table.Column<string>(nullable: true),
                    PacienteID = table.Column<int>(nullable: true),
                    FechaHora = table.Column<DateTime>(nullable: false),
                    TratamientoID = table.Column<int>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    EstaEliminada = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atencion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Atencion_Profesional_MedicoNombreUsuario",
                        column: x => x.MedicoNombreUsuario,
                        principalTable: "Profesional",
                        principalColumn: "NombreUsuario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Atencion_Paciente_PacienteID",
                        column: x => x.PacienteID,
                        principalTable: "Paciente",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Atencion_Tratamiento_TratamientoID",
                        column: x => x.TratamientoID,
                        principalTable: "Tratamiento",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Turno",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MedicoNombreUsuario = table.Column<string>(nullable: true),
                    PacienteID = table.Column<int>(nullable: true),
                    FechaHora = table.Column<DateTime>(nullable: false),
                    TratamientoID = table.Column<int>(nullable: true),
                    FechaHoraCancelacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turno", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Turno_Profesional_MedicoNombreUsuario",
                        column: x => x.MedicoNombreUsuario,
                        principalTable: "Profesional",
                        principalColumn: "NombreUsuario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Turno_Paciente_PacienteID",
                        column: x => x.PacienteID,
                        principalTable: "Paciente",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Turno_Tratamiento_TratamientoID",
                        column: x => x.TratamientoID,
                        principalTable: "Tratamiento",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atencion_MedicoNombreUsuario",
                table: "Atencion",
                column: "MedicoNombreUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Atencion_PacienteID",
                table: "Atencion",
                column: "PacienteID");

            migrationBuilder.CreateIndex(
                name: "IX_Atencion_TratamientoID",
                table: "Atencion",
                column: "TratamientoID");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_MedicoNombreUsuario",
                table: "Paciente",
                column: "MedicoNombreUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_ObraSocialID",
                table: "Paciente",
                column: "ObraSocialID");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atencion");

            migrationBuilder.DropTable(
                name: "Turno");

            migrationBuilder.DropTable(
                name: "Paciente");

            migrationBuilder.DropTable(
                name: "Obrasocial");
        }
    }
}
