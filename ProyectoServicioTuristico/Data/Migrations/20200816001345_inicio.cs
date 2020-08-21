using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoServicioTuristico.Data.Migrations
{
    public partial class inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Identidad",
                table: "Guias",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreCompleto",
                table: "Guias",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identidad",
                table: "Guias");

            migrationBuilder.DropColumn(
                name: "NombreCompleto",
                table: "Guias");
        }
    }
}
