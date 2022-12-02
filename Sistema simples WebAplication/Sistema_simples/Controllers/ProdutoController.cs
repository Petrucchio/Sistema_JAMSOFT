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
        public async Task<IActionResult> Index()
        {
            return View(await _context.Produtos.ToListAsync());
        }

        // GET: ProdutoController/Details/5
        public async Task<IActionResult> Details(int id)
        {
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

        // GET: ProdutoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProdutoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto)
        {
            if(produto.Quantidade > 0)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
                return View();
        }

        // GET: ProdutoController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
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

        // POST: ProdutoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produto produto)
        {
            if (id != produto.id)
                return NotFound();
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
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: ProdutoController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
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

        // POST: ProdutoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            var produto = await _context.Produtos.FindAsync(id);
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.id == id);
        }
    }
}
