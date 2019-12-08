using Microsoft.EntityFrameworkCore.Migrations;

namespace Medisysnae.Migrations
{
    public partial class Tratamientos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atencion_Tratamiento_TratamientoID",
                table: "Atencion");

            migrationBuilder.DropIndex(
                name: "IX_Atencion_TratamientoID",
                table: "Atencion");

            migrationBuilder.DropColumn(
                name: "TratamientoID",
                table: "Atencion");

            migrationBuilder.AddColumn<int>(
                name: "Tratamiento_ID",
                table: "Atencion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Atencion_Tratamiento_ID",
                table: "Atencion",
                column: "Tratamiento_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Atencion_Tratamiento_Tratamiento_ID",
                table: "Atencion",
                column: "Tratamiento_ID",
                principalTable: "Tratamiento",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atencion_Tratamiento_Tratamiento_ID",
                table: "Atencion");

            migrationBuilder.DropIndex(
                name: "IX_Atencion_Tratamiento_ID",
                table: "Atencion");

            migrationBuilder.DropColumn(
                name: "Tratamiento_ID",
                table: "Atencion");

            migrationBuilder.AddColumn<int>(
                name: "TratamientoID",
                table: "Atencion",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Atencion_TratamientoID",
                table: "Atencion",
                column: "TratamientoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Atencion_Tratamiento_TratamientoID",
                table: "Atencion",
                column: "TratamientoID",
                principalTable: "Tratamiento",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
