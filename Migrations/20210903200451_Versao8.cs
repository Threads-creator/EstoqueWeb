using Microsoft.EntityFrameworkCore.Migrations;

namespace EstoqueWeb.Migrations
{
    public partial class Versao8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemPedidoModel",
                columns: table => new
                {
                    IdPedido = table.Column<int>(type: "INTEGER", nullable: false),
                    IdProduto = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    ValorUnitario = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPedidoModel", x => new { x.IdPedido, x.IdProduto });
                    table.ForeignKey(
                        name: "FK_ItemPedidoModel_Pedido_IdPedido",
                        column: x => x.IdPedido,
                        principalTable: "Pedido",
                        principalColumn: "IdPedido",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPedidoModel_Produto_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "Produto",
                        principalColumn: "idProduto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedidoModel_IdProduto",
                table: "ItemPedidoModel",
                column: "IdProduto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemPedidoModel");
        }
    }
}
