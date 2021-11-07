using Microsoft.EntityFrameworkCore.Migrations;

namespace EstoqueWeb.Migrations
{
    public partial class Versao9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPedidoModel_Pedido_IdPedido",
                table: "ItemPedidoModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPedidoModel_Produto_IdProduto",
                table: "ItemPedidoModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemPedidoModel",
                table: "ItemPedidoModel");

            migrationBuilder.RenameTable(
                name: "ItemPedidoModel",
                newName: "ItemPedido");

            migrationBuilder.RenameIndex(
                name: "IX_ItemPedidoModel_IdProduto",
                table: "ItemPedido",
                newName: "IX_ItemPedido_IdProduto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemPedido",
                table: "ItemPedido",
                columns: new[] { "IdPedido", "IdProduto" });

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPedido_Pedido_IdPedido",
                table: "ItemPedido",
                column: "IdPedido",
                principalTable: "Pedido",
                principalColumn: "IdPedido",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPedido_Produto_IdProduto",
                table: "ItemPedido",
                column: "IdProduto",
                principalTable: "Produto",
                principalColumn: "idProduto",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPedido_Pedido_IdPedido",
                table: "ItemPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPedido_Produto_IdProduto",
                table: "ItemPedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemPedido",
                table: "ItemPedido");

            migrationBuilder.RenameTable(
                name: "ItemPedido",
                newName: "ItemPedidoModel");

            migrationBuilder.RenameIndex(
                name: "IX_ItemPedido_IdProduto",
                table: "ItemPedidoModel",
                newName: "IX_ItemPedidoModel_IdProduto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemPedidoModel",
                table: "ItemPedidoModel",
                columns: new[] { "IdPedido", "IdProduto" });

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPedidoModel_Pedido_IdPedido",
                table: "ItemPedidoModel",
                column: "IdPedido",
                principalTable: "Pedido",
                principalColumn: "IdPedido",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPedidoModel_Produto_IdProduto",
                table: "ItemPedidoModel",
                column: "IdProduto",
                principalTable: "Produto",
                principalColumn: "idProduto",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
