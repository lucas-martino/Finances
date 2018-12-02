using Finances.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Finances.Domain.Repository
{
    public class FinancesContext : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Orcamento> Orcamentos { get; set; }
        public DbSet<OrcamentoCategoria> OrcamentosCategoria { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vigencia> Vigencias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=Finances;user=userfin;password=pwd");
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
            
        }
    }
}