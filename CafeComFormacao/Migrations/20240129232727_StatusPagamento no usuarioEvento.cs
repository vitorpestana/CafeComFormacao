using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeComFormacao.Migrations
{
    public partial class StatusPagamentonousuarioEvento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PagamentoStatus",
                table: "Participante");

            migrationBuilder.AddColumn<int>(
                name: "PagamentoStatus",
                table: "UsuarioEvento",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PagamentoStatus",
                table: "UsuarioEvento");

            migrationBuilder.AddColumn<int>(
                name: "PagamentoStatus",
                table: "Participante",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
