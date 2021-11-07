using Microsoft.EntityFrameworkCore.Migrations;

namespace EstoqueWeb.Migrations
{
    public partial class Versao12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnderecoEntrega_Rua",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "Rua",
                table: "Endereco");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EnderecoEntrega_Rua",
                table: "Pedido",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rua",
                table: "Endereco",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
