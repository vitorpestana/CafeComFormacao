using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeComFormacao.Migrations
{
    public partial class CodigoDeVerificacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmailVerificado",
                table: "CredenciaisParticipante",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailVerificado",
                table: "CredenciaisParticipante");
        }
    }
}
