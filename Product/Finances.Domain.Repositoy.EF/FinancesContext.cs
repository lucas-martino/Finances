using Finances.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Finances.Domain.Repository
{
    public class FinancesContext : DbContext
    {
        private string ConnectionString;
        public FinancesContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Orcamento> Orcamentos { get; set; }
        public DbSet<OrcamentoCategoria> OrcamentosCategoria { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vigencia> Vigencias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(ConnectionString);             
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}