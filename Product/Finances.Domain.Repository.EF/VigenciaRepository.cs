using System;
using System.Collections.Generic;
using System.Linq;
using Finances.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Finances.Domain.Repository.EF
{
    public class VigenciaRepository : FinancesCRUDRepository<Vigencia
    >, IVigenciaRepository
    {
        public VigenciaRepository(FinancesContext dbContext)
         : base(dbContext)
        {
        }

        protected override DbSet<Vigencia> DbSet { get { return Context.Vigencias; } }

        public virtual IEnumerable<Vigencia> GetVigenciasPorUsuario(Usuario usuario)
        {
            return GetList(v => v.Usuario.Id == usuario.Id)
                .OrderByDescending(v => v.Referencia);
        }

        public virtual Vigencia GetVigencia(Usuario usuario, int referencia)
        {
            return GetOne(v => v.Usuario.Id == usuario.Id && v.Referencia == referencia);
        }

        public virtual Vigencia GetVigenciaAnterior(Vigencia vigencia)
        {
            return GetList(v => v.Referencia < vigencia.Referencia && v.Usuario.Id == vigencia.Usuario.Id)
                .OrderByDescending(v => v.Referencia)
                .FirstOrDefault();
        }
    }
}