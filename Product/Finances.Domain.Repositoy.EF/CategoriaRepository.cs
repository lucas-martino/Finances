using System;
using System.Collections.Generic;
using System.Linq;
using Finances.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Finances.Domain.Repository
{
    public class CategoriaRepository : CRUDRepository<Categoria, FinancesContext>, ICategoriaRepository
    {
        public CategoriaRepository(FinancesContext dbContext) 
            : base(dbContext)
        {
        }

        protected override DbSet<Categoria> DbSet { get { return Context.Categorias; } }

        public virtual IEnumerable<Categoria> GetCategoriaPorUsuario(Usuario usuario)
        {
            return GetList(g => g.Usuario == usuario).OrderBy(g => g.Nome);
        }
    }
}