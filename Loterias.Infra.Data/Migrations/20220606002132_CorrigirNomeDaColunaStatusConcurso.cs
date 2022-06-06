using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loterias.Infra.Data.Migrations
{
    public partial class CorrigirNomeDaColunaStatusConcurso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StatusDoConcurso",
                table: "Concurso",
                newName: "StatusConcurso");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StatusConcurso",
                table: "Concurso",
                newName: "StatusDoConcurso");
        }
    }
}
