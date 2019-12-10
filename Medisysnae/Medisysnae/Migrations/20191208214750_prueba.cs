using Microsoft.EntityFrameworkCore.Migrations;

namespace Medisysnae.Migrations
{
    public partial class prueba : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atencion_Tratamiento_Tratamiento_ID",
                table: "Atencion");

            migrationBuilder.RenameColumn(
                name: "Tratamiento_ID",
                table: "Atencion",
                newName: "Trat_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Atencion_Tratamiento_ID",
                table: "Atencion",
                newName: "IX_Atencion_Trat_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Atencion_Tratamiento_Trat_ID",
                table: "Atencion",
                column: "Trat_ID",
                principalTable: "Tratamiento",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atencion_Tratamiento_Trat_ID",
                table: "Atencion");

            migrationBuilder.RenameColumn(
                name: "Trat_ID",
                table: "Atencion",
                newName: "Tratamiento_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Atencion_Trat_ID",
                table: "Atencion",
                newName: "IX_Atencion_Tratamiento_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Atencion_Tratamiento_Tratamiento_ID",
                table: "Atencion",
                column: "Tratamiento_ID",
                principalTable: "Tratamiento",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
