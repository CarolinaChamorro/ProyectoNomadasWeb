using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoServicioTuristico.Data.Migrations
{
    public partial class Clasificacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FotografiaClasificaciones");

            migrationBuilder.AlterColumn<string>(
                name: "Fotos",
                table: "FotografiaRutas",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Fotos",
                table: "FotografiaGuias",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "ClasificacionRutas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "ClasificacionRutas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "ClasificacionRutas");

            migrationBuilder.DropColumn(
                name: "Foto",
                table: "ClasificacionRutas");

            migrationBuilder.AlterColumn<string>(
                name: "Fotos",
                table: "FotografiaRutas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Fotos",
                table: "FotografiaGuias",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "FotografiaClasificaciones",
                columns: table => new
                {
                    FotografiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClasificacionRutaId = table.Column<int>(type: "int", nullable: false),
                    DescripcionFoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fotos = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FotografiaClasificaciones", x => x.FotografiaId);
                    table.ForeignKey(
                        name: "FK_FotografiaClasificaciones_ClasificacionRutas_ClasificacionRutaId",
                        column: x => x.ClasificacionRutaId,
                        principalTable: "ClasificacionRutas",
                        principalColumn: "ClasificacionRutaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FotografiaClasificaciones_ClasificacionRutaId",
                table: "FotografiaClasificaciones",
                column: "ClasificacionRutaId");
        }
    }
}
