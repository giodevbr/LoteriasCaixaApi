using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loterias.Infra.Data.Migrations
{
    public partial class CorrigirNomeDeColunaAdicionarColunasNovasConcursoResultado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Premiacao",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataProximoConcurso",
                table: "Concurso",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Concurso",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConcursoDadosBruto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoConcurso = table.Column<int>(type: "int", nullable: false),
                    ConcursoPlanilha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcursoApi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Processado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcursoDadosBruto", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConcursoDadosBruto");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Premiacao");

            migrationBuilder.DropColumn(
                name: "DataProximoConcurso",
                table: "Concurso");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Concurso");
        }
    }
}
