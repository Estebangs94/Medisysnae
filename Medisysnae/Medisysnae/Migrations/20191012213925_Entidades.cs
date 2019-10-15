using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Medisysnae.Migrations
{
    public partial class Entidades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_Especialidad_ProfesionalNombreUsuario",
                table: "Especialidad",
                column: "ProfesionalNombreUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamiento_EspecialidadID",
                table: "Tratamiento",
                column: "EspecialidadID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tratamiento");

            migrationBuilder.DropTable(
                name: "Especialidad");

            migrationBuilder.DropTable(
                name: "Profesional");
        }
    }
}
