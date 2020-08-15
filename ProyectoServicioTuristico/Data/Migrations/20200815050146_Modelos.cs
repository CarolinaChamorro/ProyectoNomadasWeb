using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoServicioTuristico.Data.Migrations
{
    public partial class Modelos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fotografias",
                columns: table => new
                {
                    FotografiaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fotos = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fotografias", x => x.FotografiaId);
                });

            migrationBuilder.CreateTable(
                name: "Guias",
                columns: table => new
                {
                    GuiaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimerNombre = table.Column<string>(nullable: false),
                    SegundoNombre = table.Column<string>(nullable: true),
                    ApellidoPaterno = table.Column<string>(nullable: false),
                    ApellidoMaterno = table.Column<string>(nullable: true),
                    Edad = table.Column<string>(nullable: false),
                    Sexo = table.Column<int>(nullable: false),
                    Telefono = table.Column<string>(nullable: true),
                    FotoPerfil = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guias", x => x.GuiaId);
                });

            migrationBuilder.CreateTable(
                name: "Idiomas",
                columns: table => new
                {
                    IdiomaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idiomas", x => x.IdiomaId);
                });

            migrationBuilder.CreateTable(
                name: "Provincias",
                columns: table => new
                {
                    ProvinciaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincias", x => x.ProvinciaId);
                });

            migrationBuilder.CreateTable(
                name: "DetalleGuiaIdiomas",
                columns: table => new
                {
                    DetalleGuiaIdiomaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuiaId = table.Column<int>(nullable: false),
                    IdiomaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleGuiaIdiomas", x => x.DetalleGuiaIdiomaId);
                    table.ForeignKey(
                        name: "FK_DetalleGuiaIdiomas_Guias_GuiaId",
                        column: x => x.GuiaId,
                        principalTable: "Guias",
                        principalColumn: "GuiaId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DetalleGuiaIdiomas_Idiomas_IdiomaId",
                        column: x => x.IdiomaId,
                        principalTable: "Idiomas",
                        principalColumn: "IdiomaId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Cantones",
                columns: table => new
                {
                    CantonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: false),
                    ProvinciaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cantones", x => x.CantonId);
                    table.ForeignKey(
                        name: "FK_Cantones_Provincias_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalTable: "Provincias",
                        principalColumn: "ProvinciaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClasificacionRutas",
                columns: table => new
                {
                    ClasificacionRutaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: false),
                    ProvinciaId = table.Column<int>(nullable: false),
                    CantonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClasificacionRutas", x => x.ClasificacionRutaId);
                    table.ForeignKey(
                        name: "FK_ClasificacionRutas_Cantones_CantonId",
                        column: x => x.CantonId,
                        principalTable: "Cantones",
                        principalColumn: "CantonId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ClasificacionRutas_Provincias_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalTable: "Provincias",
                        principalColumn: "ProvinciaId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Rutas",
                columns: table => new
                {
                    RutaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: false),
                    PuntoPartida = table.Column<string>(nullable: false),
                    PuntoLLegada = table.Column<string>(nullable: false),
                    Precio = table.Column<double>(nullable: false),
                    DescripcionServicios = table.Column<string>(nullable: false),
                    GuiaId = table.Column<int>(nullable: false),
                    ClasificacionRutaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rutas", x => x.RutaId);
                    table.ForeignKey(
                        name: "FK_Rutas_ClasificacionRutas_ClasificacionRutaId",
                        column: x => x.ClasificacionRutaId,
                        principalTable: "ClasificacionRutas",
                        principalColumn: "ClasificacionRutaId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Rutas_Guias_GuiaId",
                        column: x => x.GuiaId,
                        principalTable: "Guias",
                        principalColumn: "GuiaId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "DetalleRutaFotos",
                columns: table => new
                {
                    DetalleRutaFotoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RutaId = table.Column<int>(nullable: false),
                    FotografiaId = table.Column<int>(nullable: false)
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
                name: "IX_Cantones_ProvinciaId",
                table: "Cantones",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClasificacionRutas_CantonId",
                table: "ClasificacionRutas",
                column: "CantonId");

            migrationBuilder.CreateIndex(
                name: "IX_ClasificacionRutas_ProvinciaId",
                table: "ClasificacionRutas",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleGuiaIdiomas_GuiaId",
                table: "DetalleGuiaIdiomas",
                column: "GuiaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleGuiaIdiomas_IdiomaId",
                table: "DetalleGuiaIdiomas",
                column: "IdiomaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleRutaFotos_FotografiaId",
                table: "DetalleRutaFotos",
                column: "FotografiaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleRutaFotos_RutaId",
                table: "DetalleRutaFotos",
                column: "RutaId");

            migrationBuilder.CreateIndex(
                name: "IX_Rutas_ClasificacionRutaId",
                table: "Rutas",
                column: "ClasificacionRutaId");

            migrationBuilder.CreateIndex(
                name: "IX_Rutas_GuiaId",
                table: "Rutas",
                column: "GuiaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleGuiaIdiomas");

            migrationBuilder.DropTable(
                name: "DetalleRutaFotos");

            migrationBuilder.DropTable(
                name: "Idiomas");

            migrationBuilder.DropTable(
                name: "Fotografias");

            migrationBuilder.DropTable(
                name: "Rutas");

            migrationBuilder.DropTable(
                name: "ClasificacionRutas");

            migrationBuilder.DropTable(
                name: "Guias");

            migrationBuilder.DropTable(
                name: "Cantones");

            migrationBuilder.DropTable(
                name: "Provincias");
        }
    }
}
