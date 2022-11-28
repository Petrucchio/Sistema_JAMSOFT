using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema_simples.Context;
using Sistema_simples.Models;

namespace Sistema_simples.Controllers
{
    public class EmprestimoController : Controller
    {
        private readonly EmprestimoContext _context;
        public EmprestimoController(EmprestimoContext context)
        {
            _context = context;
        }
        // GET: EmprestimoController
        public async Task<IActionResult> Index()
        {
            return View(await _context.emprestimos.ToListAsync());
        }

        // GET: EmprestimoController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var emprestimo = await _context.emprestimos.FirstOrDefaultAsync(m => m.Id == id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }

        // GET: EmprestimoController/Create
        public ActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.clientes, "Id", "Id");
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "id", "id");
            return View();
        }

        // POST: EmprestimoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Emprestimo emprestimo)
        {
            if (emprestimo.quantidade > 0)
            {
                if (ProdutoExist(emprestimo.ProdutoId))
                {
                    if (ClienteExist(emprestimo.Usuario))
                    {
                        emprestimo.data = DateTime.Now;
                        if (emprestimo.data_devolucao > emprestimo.data)
                        {
                            var cliente = await _context.clientes.FindAsync(emprestimo.Usuario);
                            emprestimo.Cliente = cliente.Nome;
                            if (EmprestimoValido(emprestimo.status))
                            {

                                _context.Add(emprestimo);
                                await _context.SaveChangesAsync();
                                return RedirectToAction(nameof(Index));
                            }
                        }
                    }
                }
            }
            ViewData["ClienteId"] = new SelectList(_context.clientes, "Id", "Id");
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "id", "id");
            return View();
        }

        // GET: EmprestimoController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var emprestimo = await _context.emprestimos.FindAsync(id);
            if (emprestimo == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.clientes, "Id", "Id");
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "id", "id");
            return View(emprestimo);
            
        }

        // POST: EmprestimoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Emprestimo emprestimo)
        {
            if (id != emprestimo.Id) 
                return NotFound();
           
                if (emprestimo.quantidade > 0)
                {
                    if (ProdutoExist(emprestimo.ProdutoId))
                    {
                        if (ClienteExist(emprestimo.Usuario))
                        {
                            if (emprestimo.data_devolucao > emprestimo.data)
                            {
                                var cliente = await _context.clientes.FindAsync(emprestimo.Usuario);
                                emprestimo.Cliente = cliente.Nome;
                                if (EmprestimoValido(emprestimo.status))
                                {
                                try
                                {
                                    _context.Update(emprestimo);
                                    await _context.SaveChangesAsync();
                                }
                                catch (DbUpdateConcurrencyException)
                                {
                                    if (!EmprestimoExists(emprestimo.Id))
                                        return NotFound();
                                    else
                                        throw;
                                }
                                return RedirectToAction(nameof(Index));
                                }
                            }
                        }
                    }
                }
            ViewData["ClienteId"] = new SelectList(_context.clientes, "Id", "Id");
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "id", "id");
            return View();
        }

        // GET: EmprestimoController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimo = await _context.emprestimos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }

        // POST: EmprestimoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            var emprestimo = await _context.emprestimos.FindAsync(id);
            _context.emprestimos.Remove(emprestimo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool EmprestimoValido(string emprestimo)
        {
            if (emprestimo == "Na empresa")
                return true;
            else if (emprestimo == "Emprestado")
                return true;
            else if (emprestimo == "Devolvido")
                return true;
            return false;
        }
        private bool ProdutoExist(int id)
        {
            return _context.Produtos.Any(e => e.id == id);
        }
        private bool ClienteExist(int id)
        {
            return _context.clientes.Any(e => e.Id == id);
        }
        private bool EmprestimoExists(int id)
        {
            return _context.emprestimos.Any(e => e.Id == id);
        }
    }
}
