using Microsoft.EntityFrameworkCore.Migrations;

namespace EstoqueWeb.Migrations
{
    public partial class Versao5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Endereco",
                table: "Endereco");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Endereco",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Endereco",
                table: "Endereco",
                columns: new[] { "ClienteModelIdUsuario", "Id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Endereco",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Endereco");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Endereco",
                table: "Endereco",
                column: "ClienteModelIdUsuario");
        }
    }
}
