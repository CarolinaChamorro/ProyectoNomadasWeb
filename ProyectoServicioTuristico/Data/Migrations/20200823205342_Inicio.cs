using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoServicioTuristico.Data.Migrations
{
    public partial class Inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleRutaFotos");

            migrationBuilder.DropTable(
                name: "Fotografias");

            migrationBuilder.CreateTable(
                name: "FotografiaClasificaciones",
                columns: table => new
                {
                    FotografiaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fotos = table.Column<string>(nullable: false),
                    DescripcionFoto = table.Column<string>(nullable: true),
                    ClasificacionRutaId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "FotografiaGuias",
                columns: table => new
                {
                    FotografiaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fotos = table.Column<string>(nullable: false),
                    DescripcionFoto = table.Column<string>(nullable: true),
                    GuiaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FotografiaGuias", x => x.FotografiaId);
                    table.ForeignKey(
                        name: "FK_FotografiaGuias_Guias_GuiaId",
                        column: x => x.GuiaId,
                        principalTable: "Guias",
                        principalColumn: "GuiaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FotografiaRutas",
                columns: table => new
                {
                    FotografiaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fotos = table.Column<string>(nullable: false),
                    DescripcionFoto = table.Column<string>(nullable: true),
                    RutaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FotografiaRutas", x => x.FotografiaId);
                    table.ForeignKey(
                        name: "FK_FotografiaRutas_Rutas_RutaId",
                        column: x => x.RutaId,
                        principalTable: "Rutas",
                        principalColumn: "RutaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FotografiaClasificaciones_ClasificacionRutaId",
                table: "FotografiaClasificaciones",
                column: "ClasificacionRutaId");

            migrationBuilder.CreateIndex(
                name: "IX_FotografiaGuias_GuiaId",
                table: "FotografiaGuias",
                column: "GuiaId");

            migrationBuilder.CreateIndex(
                name: "IX_FotografiaRutas_RutaId",
                table: "FotografiaRutas",
                column: "RutaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FotografiaClasificaciones");

            migrationBuilder.DropTable(
                name: "FotografiaGuias");

            migrationBuilder.DropTable(
                name: "FotografiaRutas");

            migrationBuilder.CreateTable(
                name: "Fotografias",
                columns: table => new
                {
                    FotografiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fotos = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fotografias", x => x.FotografiaId);
                });

            migrationBuilder.CreateTable(
                name: "DetalleRutaFotos",
                columns: table => new
                {
                    DetalleRutaFotoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FotografiaId = table.Column<int>(type: "int", nullable: false),
                    RutaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleRutaFotos", x => x.DetalleRutaFotoId);
                    table.ForeignKey(
                        name: "FK_DetalleRutaFotos_Fotografias_FotografiaId",
                        column: x => x.FotografiaId,
                        principalTable: "Fotografias",
                        principalColumn: "FotografiaId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DetalleRutaFotos_Rutas_RutaId",
                        column: x => x.RutaId,
                        principalTable: "Rutas",
                        principalColumn: "RutaId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleRutaFotos_FotografiaId",
                table: "DetalleRutaFotos",
                column: "FotografiaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleRutaFotos_RutaId",
                table: "DetalleRutaFotos",
                column: "RutaId");
        }
    }
}
