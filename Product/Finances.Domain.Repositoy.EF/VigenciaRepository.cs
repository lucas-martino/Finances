using System;
using System.Collections.Generic;
using System.Linq;
using Finances.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Finances.Domain.Repository
{
    public class VigenciaRepository : CRUDRepository<Vigencia, FinancesContext>, IVigenciaRepository
    {
        public VigenciaRepository(FinancesContext dbContext)
         : base(dbContext)
        {
        }

        protected override DbSet<Vigencia> DbSet { get { return Context.Vigencias; } }

        public virtual IEnumerable<Vigencia> GetVigenciasPorUsuario(Usuario usuario)
        {
            return GetList(v => v.Usuario.ID == usuario.ID);
        }

        public virtual Vigencia GetVigencia(Usuario usuario, int referencia)
        {
            return GetOne(v => v.Usuario.ID == usuario.ID && v.Referencia == referencia);
        }

        public virtual Vigencia GetVigenciaAnterior(Vigencia vigencia)
        {
            return GetList(v => v.Referencia < vigencia.Referencia).OrderByDescending(v => v.Referencia).FirstOrDefault();
        }
    }
}