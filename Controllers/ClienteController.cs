using System.Linq;
using System.Threading.Tasks;
using EstoqueWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstoqueWeb.Controllers
{
    public class ClienteController : Controller {
        private readonly EstoqueWebContext _context;

        public ClienteController(EstoqueWebContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.OrderBy(o => o.Nome).AsNoTracking().ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Cadastrar(int? id)
        {
            if(id.HasValue)
            {
                var cliente = await _context.Clientes.FindAsync(id);
                if(cliente == null)
                {
                    TempData["mensagem"] = MensagemModel.Serializar("Cliente não encontrado");
                    return RedirectToAction("Index");
                }
                return View(cliente);
            }
            return View(new ClienteModel());
        }

        private bool ClienteExiste(int id)
        {
            return _context.Clientes.Any(o => o.IdUsuario == id);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(int? id, [FromForm] ClienteModel cliente)
        {
            if(ModelState.IsValid)
            {
                if(id.HasValue)
                {
                    if(ClienteExiste(id.Value))
                    {
                        _context.Update(cliente);
                        _context.Entry(cliente).Property(o => o.Senha).IsModified = false;
                        if(await _context.SaveChangesAsync() > 0)
                        {
                            TempData["mensagem"] = MensagemModel.Serializar("Cliente alterado com sucesso");
                        }
                        else 
                        {
                            TempData["mensagem"] = MensagemModel.Serializar("Erro ao alterar Cliente", TipoMensagem.Erro);
                        }
                    }
                    else
                    {
                        TempData["mensagem"] = MensagemModel.Serializar("cliente não encontrado", TipoMensagem.Erro);
                    }
                }
                else
                {
                    _context.Add(cliente);
                    if(await _context.SaveChangesAsync() > 0)
                    {
                         TempData["mensagem"] = MensagemModel.Serializar("Cliente cadastrada com sucesso");
                    }
                    else
                    {
                         TempData["mensagem"] = MensagemModel.Serializar("Erro ao cadastrar Cliente", TipoMensagem.Erro);
                    }
                   
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(cliente);
            }
        }
    
        [HttpGet]
        public async Task<IActionResult> Excluir(int? id)
        {
            if(!id.HasValue)
            {
                TempData["mensagem"] = MensagemModel.Serializar("Cliente não informado", TipoMensagem.Erro);
                return RedirectToAction("Index");
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if(cliente == null)
            {
                TempData["mensagem"] = MensagemModel.Serializar("Cliente não encontrado", TipoMensagem.Erro);
                return RedirectToAction("Index");   
            }
            return View(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if(cliente != null)
            {
                _context.Clientes.Remove(cliente);
                if(await _context.SaveChangesAsync() > 0)
                {
                    TempData["mensagem"] = MensagemModel.Serializar("Cliente excluída com sucesso");
                }
                else
                {
                    TempData["mensagem"] = MensagemModel.Serializar("Não foi possivel excluir cliente", TipoMensagem.Erro);
                }
                return RedirectToAction("Index");
            }
            else
            {
                TempData["mensagem"] = MensagemModel.Serializar("Cliente não encontrado", TipoMensagem.Erro);
                return RedirectToAction(nameof(Index));
            }
            
        }
    }

}