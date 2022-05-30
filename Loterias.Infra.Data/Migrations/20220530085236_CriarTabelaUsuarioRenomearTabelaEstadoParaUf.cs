using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loterias.Infra.Data.Migrations
{
    public partial class CriarTabelaUsuarioRenomearTabelaEstadoParaUf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Municipio_Estado_EstadoId",
                table: "Municipio");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.RenameColumn(
                name: "EstadoId",
                table: "Municipio",
                newName: "UfId");

            migrationBuilder.RenameIndex(
                name: "IX_Municipio_EstadoId",
                table: "Municipio",
                newName: "IX_Municipio_UfId");

            migrationBuilder.CreateTable(
                name: "Uf",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IbgeId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Sigla = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uf", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Municipio_Uf_UfId",
                table: "Municipio",
                column: "UfId",
                principalTable: "Uf",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Municipio_Uf_UfId",
                table: "Municipio");

            migrationBuilder.DropTable(
                name: "Uf");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.RenameColumn(
                name: "UfId",
                table: "Municipio",
                newName: "EstadoId");

            migrationBuilder.RenameIndex(
                name: "IX_Municipio_UfId",
                table: "Municipio",
                newName: "IX_Municipio_EstadoId");

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IbgeId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sigla = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Municipio_Estado_EstadoId",
                table: "Municipio",
                column: "EstadoId",
                principalTable: "Estado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
