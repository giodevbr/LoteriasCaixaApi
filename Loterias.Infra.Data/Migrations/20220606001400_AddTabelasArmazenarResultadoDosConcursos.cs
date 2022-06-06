using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loterias.Infra.Data.Migrations
{
    public partial class AddTabelasArmazenarResultadoDosConcursos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Concurso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    TipoConcurso = table.Column<int>(type: "int", nullable: false),
                    DataSorteio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusDoConcurso = table.Column<int>(type: "int", nullable: false),
                    ValorArrecadado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorAcumulado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorSorteado = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concurso", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConcursoLocalidade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ConcursoId = table.Column<int>(type: "int", nullable: false),
                    MunicipioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcursoLocalidade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConcursoLocalidade_Concurso_Id",
                        column: x => x.Id,
                        principalTable: "Concurso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConcursoLocalidade_Municipio_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Premiacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConcursoId = table.Column<int>(type: "int", nullable: false),
                    Pontos = table.Column<int>(type: "int", nullable: false),
                    Ganhadores = table.Column<int>(type: "int", nullable: false),
                    ValorPagoPorGanhador = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorPagoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premiacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Premiacao_Concurso_ConcursoId",
                        column: x => x.ConcursoId,
                        principalTable: "Concurso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PremiacaoLocalidade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConcursoId = table.Column<int>(type: "int", nullable: false),
                    MunicipioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PremiacaoLocalidade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PremiacaoLocalidade_Concurso_ConcursoId",
                        column: x => x.ConcursoId,
                        principalTable: "Concurso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PremiacaoLocalidade_Municipio_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resultado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ConcursoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resultado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resultado_Concurso_Id",
                        column: x => x.Id,
                        principalTable: "Concurso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResultadoNumeroSorteado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResultadoId = table.Column<int>(type: "int", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultadoNumeroSorteado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResultadoNumeroSorteado_Resultado_ResultadoId",
                        column: x => x.ResultadoId,
                        principalTable: "Resultado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConcursoLocalidade_MunicipioId",
                table: "ConcursoLocalidade",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_Premiacao_ConcursoId",
                table: "Premiacao",
                column: "ConcursoId");

            migrationBuilder.CreateIndex(
                name: "IX_PremiacaoLocalidade_ConcursoId",
                table: "PremiacaoLocalidade",
                column: "ConcursoId");

            migrationBuilder.CreateIndex(
                name: "IX_PremiacaoLocalidade_MunicipioId",
                table: "PremiacaoLocalidade",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadoNumeroSorteado_ResultadoId",
                table: "ResultadoNumeroSorteado",
                column: "ResultadoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConcursoLocalidade");

            migrationBuilder.DropTable(
                name: "Premiacao");

            migrationBuilder.DropTable(
                name: "PremiacaoLocalidade");

            migrationBuilder.DropTable(
                name: "ResultadoNumeroSorteado");

            migrationBuilder.DropTable(
                name: "Resultado");

            migrationBuilder.DropTable(
                name: "Concurso");
        }
    }
}
