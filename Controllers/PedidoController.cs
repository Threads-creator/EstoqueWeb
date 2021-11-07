using System.Linq;
using System.Threading.Tasks;
using EstoqueWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstoqueWeb.Controllers
{
    public class PedidoController : Controller {
        private readonly EstoqueWebContext _context;

        public PedidoController(EstoqueWebContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? cid)
        {
            if(cid.HasValue)
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.IdUsuario == cid);
                if(cliente != null)
                {
                    var pedidos = await _context.Pedidos
                        .Where(p => p.IdCliente == cid)
                        .OrderByDescending(x => x.IdPedido)
                        .AsNoTracking().ToListAsync();
                    
                    ViewBag.Cliente = cliente;
                    return View(pedidos);
                }
                else
                {
                     TempData["mensagem"] = MensagemModel.Serializar("Cliente não encontrado !", TipoMensagem.Erro);
                    return RedirectToAction("Index", "Cliente");
                }
            }else
            {
                TempData["mensagem"] = MensagemModel.Serializar("Nenhum Cliente informado !", TipoMensagem.Erro);
                return RedirectToAction("Index", "Cliente");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Cadastrar(int? cid)
        {
            if(cid.HasValue)
            {
                var cliente = await _context.Clientes.FindAsync(cid);
                if(cliente != null)
                {
                    _context.Entry(cliente).Collection(c => c.Pedidos).Load();
                    PedidoModel pedido = null;
                    if(_context.Pedidos.Any(p => p.IdCliente == cid && !p.DataPedido.HasValue))
                    {
                        pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.IdCliente == cid && !p.DataPedido.HasValue);
                    }
                    else
                    {
                        pedido = new PedidoModel { IdCliente = cid.Value, ValorTotal = 0};
                        cliente.Pedidos.Add(pedido);
                        await _context.SaveChangesAsync();
                    }
                    return RedirectToAction("Index", "ItemPedido", new { ped = pedido.IdPedido});
                }
                TempData["mensagem"] = MensagemModel.Serializar("Cliente não encontrado !", TipoMensagem.Erro);
                return RedirectToAction("Index", "Cliente");
            }
            TempData["mensagem"] = MensagemModel.Serializar("Cliente não informado !", TipoMensagem.Erro);
            return RedirectToAction("Index", "Cliente");
        }

        private bool PedidoExiste(int id)
        {
            return _context.Pedidos.Any(o => o.IdPedido == id);
        }

        // [HttpPost]
        // public async Task<IActionResult> Cadastrar(int? id, [FromForm] PedidoModel cliente)
        // {
        //     if(ModelState.IsValid)
        //     {
        //         if(id.HasValue)
        //         {
        //             if(PedidoExiste(id.Value))
        //             {
        //                 _context.Update(cliente);
        //                 _context.Entry(cliente).Property(o => o.Senha).IsModified = false;
        //                 if(await _context.SaveChangesAsync() > 0)
        //                 {
        //                     TempData["mensagem"] = MensagemModel.Serializar("Pedido alterado com sucesso");
        //                 }
        //                 else 
        //                 {
        //                     TempData["mensagem"] = MensagemModel.Serializar("Erro ao alterar Pedido", TipoMensagem.Erro);
        //                 }
        //             }
        //             else
        //             {
        //                 TempData["mensagem"] = MensagemModel.Serializar("cliente não encontrado", TipoMensagem.Erro);
        //             }
        //         }
        //         else
        //         {
        //             _context.Add(cliente);
        //             if(await _context.SaveChangesAsync() > 0)
        //             {
        //                  TempData["mensagem"] = MensagemModel.Serializar("Pedido cadastrada com sucesso");
        //             }
        //             else
        //             {
        //                  TempData["mensagem"] = MensagemModel.Serializar("Erro ao cadastrar Pedido", TipoMensagem.Erro);
        //             }
                   
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     else
        //     {
        //         return View(cliente);
        //     }
        // }
    
        [HttpGet]
        public async Task<IActionResult> Excluir(int? id)
        {
            if(!id.HasValue)
            {
                TempData["mensagem"] = MensagemModel.Serializar("Pedido não informado", TipoMensagem.Erro);
                return RedirectToAction("Index");
            }

            var cliente = await _context.Pedidos.FindAsync(id);
            if(cliente == null)
            {
                TempData["mensagem"] = MensagemModel.Serializar("Pedido não encontrado", TipoMensagem.Erro);
                return RedirectToAction("Index");   
            }
            return View(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(int id)
        {
            var cliente = await _context.Pedidos.FindAsync(id);
            if(cliente != null)
            {
                _context.Pedidos.Remove(cliente);
                if(await _context.SaveChangesAsync() > 0)
                {
                    TempData["mensagem"] = MensagemModel.Serializar("Pedido excluída com sucesso");
                }
                else
                {
                    TempData["mensagem"] = MensagemModel.Serializar("Não foi possivel excluir cliente", TipoMensagem.Erro);
                }
                return RedirectToAction("Index");
            }
            else
            {
                TempData["mensagem"] = MensagemModel.Serializar("Pedido não encontrado", TipoMensagem.Erro);
                return RedirectToAction(nameof(Index));
            }
            
        }
    }

}