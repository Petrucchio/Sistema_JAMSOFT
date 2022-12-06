using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using Sistema_simples.Context;
using Sistema_simples.Models;

namespace Sistema_simples.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly EmprestimoContext _context;
        public UsuarioController(EmprestimoContext context)
        {
            _context = context;

        }
        // GET: UsuarioController
        public ActionResult Login()
        {
            return View();
        }

        // GET: UsuarioController/Details/5
        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            var status = _context.Usuario.Where(e => e.User == usuario.User && e.Password == usuario.Password).ToList();
            if(!status.Any())
            {
                ViewBag.LoginStatus = "0";
            }
            else
            {
                var userlocate = status.ElementAt(0);
                return RedirectToAction("SuccessPage", userlocate);
            }
            return View();
        }
        public ActionResult SuccessPage(Usuario usuario)
        {
            ViewData["LoginStatus"] = usuario.Nome;
            ViewData["id"] = usuario.Id;
            return View(usuario);
        }

        // GET: UsuarioController/Create
        public ActionResult Subscribe()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(Usuario usuario)
        {
            if (!UserExists(usuario))
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            ViewBag.LoginStatus = "0";
            return View();
                
        }
        private bool UserExists(Usuario usuario)
        {
            return _context.Usuario.Any(e => e.User == usuario.User);
        }

    }
}
