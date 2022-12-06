using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_simples.Context;
using Sistema_simples.Models;

namespace Sistema_simples.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly EmprestimoContext _context;
        public ProdutoController(EmprestimoContext context)
        {
            _context = context;

        }
        // GET: ProdutoController
        public async Task<IActionResult> Index(int usuarioId, Usuario usuario1)
        {
            if (usuario1.Id != 0)
                usuarioId = usuario1.Id;

            if (usuarioId != 0)
            {
                var usuario = await _context.Usuario.FirstOrDefaultAsync(m => m.Id == usuarioId);
                ViewData["LoginStatus"] = usuario.Nome;
                ViewData["id"] = usuario.Id;
                return View(await _context.Produtos.ToListAsync());
            }
            return RedirectToAction("Login","Usuario");
        }

        // GET: ProdutoController/Details/5
        public async Task<IActionResult> Details(int id, int usuarioId)
        {
            if (usuarioId != 0)
            {
                var usuario = await _context.Usuario.FirstOrDefaultAsync(m => m.Id == usuarioId);
                ViewData["LoginStatus"] = usuario.Nome;
                ViewData["id"] = usuario.Id;
                if (id == null)
                {
                    return NotFound();
                }
                var produto = await _context.Produtos.FirstOrDefaultAsync(m => m.id == id);
                if (produto == null)
                {
                    return NotFound();
                }

                return View(produto);
            }
            return RedirectToAction("Login", "Usuario");
        }

        // GET: ProdutoController/Create
        public async Task<IActionResult> Create(int usuarioId)
        {
            if (usuarioId != 0)
            {
                var usuario = await _context.Usuario.FirstOrDefaultAsync(m => m.Id == usuarioId);
                ViewData["LoginStatus"] = usuario.Nome;
                ViewData["id"] = usuario.Id;
                return View();
            }
            return RedirectToAction("Login", "Usuario");
        }

        // POST: ProdutoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto, int usuarioId)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(m => m.Id == usuarioId);
            ViewData["LoginStatus"] = usuario.Nome;
            ViewData["id"] = usuario.Id;
            if (produto.Quantidade > 0)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", usuario);

            }
                return View();
        }

        // GET: ProdutoController/Edit/5
        public async Task<IActionResult> Edit(int id, int usuarioId)
        {
            if (usuarioId != 0)
            {
                var usuario = await _context.Usuario.FirstOrDefaultAsync(m => m.Id == usuarioId);
                ViewData["LoginStatus"] = usuario.Nome;
                ViewData["id"] = usuario.Id;
                if (id == null)
                {
                    return NotFound();
                }
                var produto = await _context.Produtos.FindAsync(id);
                if (produto == null)
                {
                    return NotFound();
                }
                return View(produto);
            }
            return RedirectToAction("Login", "Usuario");
        }

        // POST: ProdutoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produto produto, int usuarioId)
        {
            if (id != produto.id)
                return NotFound();
            var usuario = await _context.Usuario.FirstOrDefaultAsync(m => m.Id == usuarioId);
            ViewData["LoginStatus"] = usuario.Nome;
            ViewData["id"] = usuario.Id;
            if (ModelState.IsValid && produto.Quantidade > 0)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction("Index", usuario);
            }
            return View(produto);
        }

        // GET: ProdutoController/Delete/5
        public async Task<IActionResult> Delete(int id, int usuarioId)
        {
            if (usuarioId != 0)
            {
                var usuario = await _context.Usuario.FirstOrDefaultAsync(m => m.Id == usuarioId);
                ViewData["LoginStatus"] = usuario.Nome;
                ViewData["id"] = usuario.Id;
                if (id == null)
                {
                    return NotFound();
                }

                var produto = await _context.Produtos
                    .FirstOrDefaultAsync(m => m.id == id);
                if (produto == null)
                {
                    return NotFound();
                }

                return View(produto);
            }
            return RedirectToAction("Login", "Usuario");
        }

        // POST: ProdutoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection, int usuarioId)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(m => m.Id == usuarioId);
            ViewData["LoginStatus"] = usuario.Nome;
            ViewData["id"] = usuario.Id;
            var produto = await _context.Produtos.FindAsync(id);
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", usuario);
        }
        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.id == id);
        }
    }
}
