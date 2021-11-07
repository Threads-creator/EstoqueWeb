using System.Linq;
using System.Threading.Tasks;
using EstoqueWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EstoqueWeb.Controllers
{
    public class ProdutoController : Controller {
        private readonly EstoqueWebContext _context;

        public ProdutoController(EstoqueWebContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // define o retorno de um view que recebe um set do banco da dados. Esse Set é ordenado pelo nome (OrderBy), e sua chamada é feita uma unica vez (AsNoTracking)
            // Include => define elementos que serão incluidos durante o select no banco de dados. Nesse caso categoria associada a cada produto
            return View(await _context.Produtos.OrderBy(o => o.Nome).Include(o => o.Categoria).AsNoTracking().ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Cadastrar(int? id)
        {
            // pegando categorias cadastradas no BD
            var categorias = _context.Categorias.OrderBy(o => o.Nome).AsNoTracking().ToList();
            // definindo uma select list. Uma lista populada por categorias que ao selecionada retorna o Id, e exibe o Nome graficamente ao usuario
            var categoriasSelectList = new SelectList(categorias, nameof(CategoriaModel.IdCategoria), nameof(CategoriaModel.Nome));
            
            ViewBag.Categorias = categoriasSelectList;
            if(id.HasValue)
            {
                var produto = await _context.Produtos.FindAsync(id);
                if(produto == null)
                {
                    return NotFound();
                }
                return View(produto);
            }
            return View(new ProdutoModel());
        }

        private bool ProdutoExiste(int id)
        {
            return _context.Produtos.Any(o => o.idProduto == id);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(int? id, [FromForm] ProdutoModel produto)
        {
            if(ModelState.IsValid)
            {
                if(id.HasValue)
                {
                    if(ProdutoExiste(id.Value))
                    {
                        _context.Update(produto);
                        if(await _context.SaveChangesAsync() > 0)
                        {
                            TempData["mensagem"] = MensagemModel.Serializar("Produto alterado com sucesso");
                        }
                        else 
                        {
                            TempData["mensagem"] = MensagemModel.Serializar("Erro ao alterar Produto", TipoMensagem.Erro);
                        }
                    }
                    else
                    {
                        TempData["mensagem"] = MensagemModel.Serializar("Produto nao encontrado", TipoMensagem.Erro);
                    }
                }
                else
                {
                    _context.Add(produto);
                    if(await _context.SaveChangesAsync() > 0)
                    {
                         TempData["mensagem"] = MensagemModel.Serializar("Produto cadastrado com sucesso");
                    }
                    else
                    {
                         TempData["mensagem"] = MensagemModel.Serializar("Erro ao cadastrar Produto", TipoMensagem.Erro);
                    }
                   
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(produto);
            }
        }
    
        [HttpGet]
        public async Task<IActionResult> Excluir(int? id)
        {
            if(!id.HasValue)
            {
                TempData["mensagem"] = MensagemModel.Serializar("Produto não informado", TipoMensagem.Erro);
                return RedirectToAction("Index");
            }

            var produto = await _context.Produtos.FindAsync(id);
            if(produto == null)
            {
                TempData["mensagem"] = MensagemModel.Serializar("Produto não encontrado", TipoMensagem.Erro);
                return RedirectToAction("Index");   
            }
            return View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if(produto != null)
            {
                _context.Produtos.Remove(produto);
                if(await _context.SaveChangesAsync() > 0)
                {
                    TempData["mensagem"] = MensagemModel.Serializar("Produto excluído com sucesso");
                }
                else
                {
                    TempData["mensagem"] = MensagemModel.Serializar("Não foi possivel excluir Produto", TipoMensagem.Erro);
                }
            }
            else
            {
                TempData["mensagem"] = MensagemModel.Serializar("Produto não encontrado", TipoMensagem.Erro);
            }
            return RedirectToAction(nameof(Index));
            
        }
    }

}