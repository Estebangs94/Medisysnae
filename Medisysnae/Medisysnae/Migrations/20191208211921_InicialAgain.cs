using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Medisysnae.Migrations
{
    public partial class InicialAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Antecedente",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    EstaActivo = table.Column<bool>(nullable: false),
                    EsListaOpciones = table.Column<bool>(nullable: false),
                    Orden = table.Column<int>(nullable: false),
                    EsTextArea = table.Column<bool>(nullable: false),
                    EsTitulo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Antecedente", x => x.ID);
                });

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
                name: "Profesional",
                columns: table => new
                {
                    NombreUsuario = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    EstaHabilitado = table.Column<bool>(nullable: false),
                    EsAdministrador = table.Column<bool>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    Apellido = table.Column<string>(nullable: false),
                    Matricula = table.Column<string>(nullable: true),
                    Telefono = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesional", x => x.NombreUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Especialidad",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false),
                    ProfesionalNombreUsuario = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidad", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Especialidad_Profesional_ProfesionalNombreUsuario",
                        column: x => x.ProfesionalNombreUsuario,
                        principalTable: "Profesional",
                        principalColumn: "NombreUsuario",
                        onDelete: ReferentialAction.Restrict);
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
                    Obrasocial_ID = table.Column<int>(nullable: false),
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
                        name: "FK_Paciente_Obrasocial_Obrasocial_ID",
                        column: x => x.Obrasocial_ID,
                        principalTable: "Obrasocial",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tratamiento",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    CodigoNM = table.Column<int>(nullable: true),
                    EspecialidadID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tratamiento", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tratamiento_Especialidad_EspecialidadID",
                        column: x => x.EspecialidadID,
                        principalTable: "Especialidad",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AntecedentePaciente",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AntecedenteID = table.Column<int>(nullable: true),
                    PacienteID = table.Column<int>(nullable: true),
                    MedicoNombreUsuario = table.Column<string>(nullable: true),
                    ValorString = table.Column<string>(nullable: true),
                    ValorBool = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntecedentePaciente", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AntecedentePaciente_Antecedente_AntecedenteID",
                        column: x => x.AntecedenteID,
                        principalTable: "Antecedente",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AntecedentePaciente_Profesional_MedicoNombreUsuario",
                        column: x => x.MedicoNombreUsuario,
                        principalTable: "Profesional",
                        principalColumn: "NombreUsuario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AntecedentePaciente_Paciente_PacienteID",
                        column: x => x.PacienteID,
                        principalTable: "Paciente",
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
                    Tratamiento_ID = table.Column<int>(nullable: false),
                    Diagnostico = table.Column<string>(nullable: true),
                    Titulo = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    EstaEliminada = table.Column<bool>(nullable: false),
                    Medicacion = table.Column<string>(nullable: true),
                    Comentario = table.Column<string>(nullable: true)
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
                        name: "FK_Atencion_Tratamiento_Tratamiento_ID",
                        column: x => x.Tratamiento_ID,
                        principalTable: "Tratamiento",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_AntecedentePaciente_AntecedenteID",
                table: "AntecedentePaciente",
                column: "AntecedenteID");

            migrationBuilder.CreateIndex(
                name: "IX_AntecedentePaciente_MedicoNombreUsuario",
                table: "AntecedentePaciente",
                column: "MedicoNombreUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_AntecedentePaciente_PacienteID",
                table: "AntecedentePaciente",
                column: "PacienteID");

            migrationBuilder.CreateIndex(
                name: "IX_Atencion_MedicoNombreUsuario",
                table: "Atencion",
                column: "MedicoNombreUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Atencion_PacienteID",
                table: "Atencion",
                column: "PacienteID");

            migrationBuilder.CreateIndex(
                name: "IX_Atencion_Tratamiento_ID",
                table: "Atencion",
                column: "Tratamiento_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Especialidad_ProfesionalNombreUsuario",
                table: "Especialidad",
                column: "ProfesionalNombreUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_MedicoNombreUsuario",
                table: "Paciente",
                column: "MedicoNombreUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_Obrasocial_ID",
                table: "Paciente",
                column: "Obrasocial_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamiento_EspecialidadID",
                table: "Tratamiento",
                column: "EspecialidadID");

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
                name: "AntecedentePaciente");

            migrationBuilder.DropTable(
                name: "Atencion");

            migrationBuilder.DropTable(
                name: "Turno");

            migrationBuilder.DropTable(
                name: "Antecedente");

            migrationBuilder.DropTable(
                name: "Paciente");

            migrationBuilder.DropTable(
                name: "Tratamiento");

            migrationBuilder.DropTable(
                name: "Obrasocial");

            migrationBuilder.DropTable(
                name: "Especialidad");

            migrationBuilder.DropTable(
                name: "Profesional");
        }
    }
}
