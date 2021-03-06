using System;
using System.Collections.Generic;
using System.Linq;
using Finances.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Finances.Domain.Repository.EF
{
    public class CategoriaRepository : FinancesCRUDRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(FinancesContext dbContext)
            : base(dbContext)
        {
        }

        protected override DbSet<Categoria> DbSet { get { return Context.Categorias; } }

        public Categoria GetCategoriaPorNomeEUsuario(string nome, Usuario usuario)
        {
            return GetList(c => c.Usuario.Id == usuario.Id && c.Nome.ToLower() == nome.ToLower())
                .Include(c => c.Pai)
                .FirstOrDefault();
        }

        public virtual IEnumerable<Categoria> GetCategoriaPorUsuario(Usuario usuario)
        {
            return GetList(c => c.Usuario.Id == usuario.Id)
                .Include(c => c.Pai)
                .OrderBy(c => c.Pai != null ? string.Format("{0}{1}", c.Pai.Nome, c.Nome) : c.Nome);
        }

        public virtual IEnumerable<Categoria> GetCategoriaLevel1PorUsuario(Usuario usuario)
        {
            return GetList(c => c.Usuario.Id == usuario.Id && c.Pai == null)
                .OrderBy(c => c.Nome);
        }
    }
}