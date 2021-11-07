using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EstoqueWeb.Models 
{
    public class CategoriaModel
    {
        [Key]
        public int IdCategoria {get; set;}
        [Required, MaxLength(100)]
        public string Nome {get; set;}

        public ICollection<ProdutoModel> Produtos { get; set; }
    }
}