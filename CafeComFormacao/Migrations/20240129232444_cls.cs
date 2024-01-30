using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeComFormacao.Migrations
{
    public partial class cls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PagamentoStatus",
                table: "Participante",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PagamentoStatus",
                table: "Participante");
        }
    }
}
