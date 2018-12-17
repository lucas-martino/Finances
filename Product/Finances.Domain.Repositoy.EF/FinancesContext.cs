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
            modelBuilder.Entity<Usuario>().Property(U => U.Nome).HasMaxLength(50);
            modelBuilder.Entity<Usuario>().Property(u => u.Login).HasMaxLength(50);
            modelBuilder.Entity<Usuario>().Property(u => u.Senha).HasMaxLength(256);

            modelBuilder.Entity<Categoria>().Property(c => c.Nome).HasMaxLength(15);
            modelBuilder.Entity<Categoria>().Property(c => c.Cor).HasMaxLength(50);
            modelBuilder.Entity<Categoria>().Property(c => c.Icone).HasMaxLength(50);

            base.OnModelCreating(modelBuilder);
        }
    }
}