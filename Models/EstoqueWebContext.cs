using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EstoqueWeb.Models 
{
    /* Classe que instancia o banco de dadso sql lite. Precisa implementar DbContext*/
    public class EstoqueWebContext : DbContext
    {
        /* Define um conjutno de dados do tipo CategoiraModel a ser instanciado no BD*/
        public DbSet<CategoriaModel> Categorias {get; set;}

        public DbSet<ProdutoModel> Produtos {get; set;}

        public DbSet<ClienteModel> Clientes {get; set;}
        public DbSet<PedidoModel> Pedidos {get; set;}
        public DbSet<ItemPedidoModel> ItensPedidos {get; set;}

        public EstoqueWebContext(DbContextOptions<EstoqueWebContext> options) : base(options)
        {

        }

        /* Define customizações da criação de entidades no BD*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // define o nome da tabela para o objeto CategoriaMoedel
            modelBuilder.Entity<CategoriaModel>().ToTable("Categoria");

            modelBuilder.Entity<ProdutoModel>().ToTable("Produto");
            // definindo foreign key e PK Composta para tabela
            modelBuilder.Entity<ClienteModel>()
                .OwnsMany( c => c.Enderecos, e => {
                    e.WithOwner().HasForeignKey("IdUsuario");
                    e.HasKey("IdUsuario", "IdEndereco");
                });
            // definindo valor default via sql
            modelBuilder.Entity<UsuarioModel>().Property(u => u.DataCadastro)
                .HasDefaultValueSql("datetime('now', 'localtime', 'start of day')")
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            
            // definindo valor default via variavel padrao
            modelBuilder.Entity<ProdutoModel>().Property(p => p.Estoque)
                .HasDefaultValue(0);

            modelBuilder.Entity<PedidoModel>()
                .OwnsOne(p => p.EnderecoEntrega, e => 
                {
                    e.Ignore(e => e.IdEndereco);
                    e.Ignore(e => e.Selecionado);
                    //torna Endereço atributos contidos na tabela Pedido diretamente
                    e.ToTable("Pedido");
                });

            modelBuilder.Entity<ItemPedidoModel>()
                .HasKey(ip => new { ip.IdPedido, ip.IdProduto });
        }
    }
}