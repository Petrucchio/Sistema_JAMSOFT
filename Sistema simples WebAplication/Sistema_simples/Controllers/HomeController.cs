using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_simples.Context;
using Sistema_simples.Models;
using System.Diagnostics;

namespace Sistema_simples.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly EmprestimoContext _context;
        public HomeController(ILogger<HomeController> logger, EmprestimoContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(m => m.Id == id);
            if(usuario != null) { 
            ViewData["LoginStatus"] = usuario.Nome;
            ViewData["id"] = usuario.Id;
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}