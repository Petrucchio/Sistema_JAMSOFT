using Microsoft.EntityFrameworkCore;
using Sistema_simples.Models;
using System.Collections.Generic;

namespace Sistema_simples.Context
{
    public class EmprestimoContext : DbContext
    {
        public EmprestimoContext(DbContextOptions<EmprestimoContext> options) : base(options)
        {
        }

        public DbSet<Cliente> clientes { get; set; }
        public DbSet<Emprestimo> emprestimos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
    }

}

