using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_simples.Context;
using Sistema_simples.Models;

namespace Sistema_simples.Controllers
{
    public class ClienteController : Controller
    {

        private readonly EmprestimoContext _context;
        public ClienteController(EmprestimoContext context)
        {
            _context = context;

        }
        // GET: ClientesController
        public async Task<IActionResult> Index(int usuarioId,Usuario usuario1)
        {
            if(usuario1.Id != 0)
            usuarioId = usuario1.Id;

            if (usuarioId != 0)
            {
                var usuario = await _context.Usuario.FirstOrDefaultAsync(m => m.Id == usuarioId);
                ViewData["LoginStatus"] = usuario.Nome;
                ViewData["id"] = usuario.Id;
                return View(await _context.clientes.ToListAsync());
            }
            return RedirectToAction("Login", "Usuario");
        }

        // GET: ClientesController/Details/5
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
                var cliente = await _context.clientes.FirstOrDefaultAsync(m => m.Id == id);
                if (cliente == null)
                {
                    return NotFound();
                }

                return View(cliente);
            }
            return RedirectToAction("Login", "Usuario");
        }

        // GET: ClientesController/Create
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

        // POST: ClientesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente, int usuarioId)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(m => m.Id == usuarioId);
            ViewData["LoginStatus"] = usuario.Nome;
            ViewData["id"] = usuario.Id;
            cliente.Cpf = replaceCPF(cliente.Cpf);
            if (CpfExist(cliente.Cpf))
            {
                ViewData["CPF"] = "O CPF informado já está cadastrado";
                return View();
            }

            if (IsValid(cliente.Cpf))
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index",usuario);
            }
                return View();
        }

        // GET: ClientesController/Edit/5
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
                var cliente = await _context.clientes.FindAsync(id);
                if (cliente == null)
                {
                    return NotFound();
                }
                ViewData["dados"] = cliente.Cpf;
                return View(cliente);
            }
            return RedirectToAction("Login", "Usuario");
        }

        // POST: ClientesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cliente cliente, int usuarioId, string dados)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }
            var usuario = await _context.Usuario.FirstOrDefaultAsync(m => m.Id == usuarioId);
            ViewData["LoginStatus"] = usuario.Nome;
            ViewData["id"] = usuario.Id;
            
                cliente.Cpf = replaceCPF(cliente.Cpf);

            if (IsValid(cliente.Cpf))
            {

            
                if (dados != cliente.Cpf)
                {
                    if (CpfExist(cliente.Cpf))
                    {
                        ViewData["CPF"] = "O CPF informado já está cadastrado";
                        return View();
                    }
                }
                try
                {

                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", usuario);
            }
            return View(cliente);
        }

        // GET: ClientesController/Delete/5
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

                var cliente = await _context.clientes.FirstOrDefaultAsync(m => m.Id == id);
                if (cliente == null)
                {
                    return NotFound();
                }

                return View(cliente);
            }
            return RedirectToAction("Login", "Usuario");
        }

        // POST: ClientesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection, int usuarioId)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(m => m.Id == usuarioId);
            ViewData["LoginStatus"] = usuario.Nome;
            ViewData["id"] = usuario.Id;
            var cliente = await _context.clientes.FindAsync(id);
            _context.clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", usuario);
        }
        public static bool CpfValidation(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            
            if (cpf.Length != 11)
                return false;
            if (cpf == "00000000000" ||
                cpf == "11111111111" ||
                cpf == "22222222222" ||
                cpf == "33333333333" ||
                cpf == "44444444444" ||
                cpf == "55555555555" ||
                cpf == "66666666666" ||
                cpf == "77777777777" ||
                cpf == "88888888888" ||
                cpf == "99999999999")
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);

        }

        public static bool CnpjValidation(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }
        public bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return false;
            if (value.ToString().Length >= 14 && value.ToString().Contains("/"))
                return CnpjValidation(value.ToString());
            return CpfValidation(value.ToString());
        }
        private bool ClienteExists(int id)
        {
            return _context.clientes.Any(e => e.Id == id);
        }
        private bool CpfExist(string cpf)
        {
            return _context.clientes.Any(e => e.Cpf == cpf);
        }
        private string replaceCPF(string cpf)
        {
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            return cpf;
        }

    }
}
