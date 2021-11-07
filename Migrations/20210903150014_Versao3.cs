using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EstoqueWeb.Migrations
{
    public partial class Versao3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Categoria_idCategoria",
                table: "Produto");

            migrationBuilder.RenameColumn(
                name: "idCategoria",
                table: "Produto",
                newName: "IdCategoria");

            migrationBuilder.RenameIndex(
                name: "IX_Produto_idCategoria",
                table: "Produto",
                newName: "IX_Produto_IdCategoria");

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "INTEGER", nullable: false),
                    CPF = table.Column<string>(type: "char(14)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Endereco_Logradouro = table.Column<string>(type: "TEXT", nullable: true),
                    Endereco_Numero = table.Column<string>(type: "TEXT", nullable: true),
                    Endereco_Complemento = table.Column<string>(type: "TEXT", nullable: true),
                    Endereco_Bairro = table.Column<string>(type: "TEXT", nullable: true),
                    Endereco_Cidade = table.Column<string>(type: "TEXT", nullable: true),
                    Endereco_Rua = table.Column<string>(type: "TEXT", nullable: true),
                    Endereco_Estado = table.Column<string>(type: "TEXT", nullable: true),
                    Endereco_CEP = table.Column<string>(type: "char(8)", nullable: true),
                    Endereco_Referencia = table.Column<string>(type: "TEXT", nullable: true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Senha = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.IdUsuario);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Categoria_IdCategoria",
                table: "Produto",
                column: "IdCategoria",
                principalTable: "Categoria",
                principalColumn: "IdCategoria",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Categoria_IdCategoria",
                table: "Produto");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.RenameColumn(
                name: "IdCategoria",
                table: "Produto",
                newName: "idCategoria");

            migrationBuilder.RenameIndex(
                name: "IX_Produto_IdCategoria",
                table: "Produto",
                newName: "IX_Produto_idCategoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Categoria_idCategoria",
                table: "Produto",
                column: "idCategoria",
                principalTable: "Categoria",
                principalColumn: "IdCategoria",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
