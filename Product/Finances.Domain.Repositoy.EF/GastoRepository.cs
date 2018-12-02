using System;
using System.Collections.Generic;
using System.Linq;
using Finances.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Finances.Domain.Repository
{
    public class GastoRepository : CRUDRepository<Gasto, FinancesContext>, IGastoRepository
    {
        public GastoRepository(FinancesContext dbContext) 
            : base(dbContext)
        {
        }

        protected override DbSet<Gasto> DbSet { get { return Context.Gastos; } }

        public virtual IEnumerable<Gasto> GetGastosPorVigencia(Vigencia vigencia)
        {
            return GetList(g => g.Vigencia.ID == vigencia.ID).OrderByDescending(g => g.Data).ThenByDescending(g => g.ID);
        }

        public virtual decimal GetGastoTotalPorVigencia(Vigencia vigencia)
        {
            return GetList(g => g.Vigencia.ID == vigencia.ID).Sum(g => g.Valor);
        }

        public virtual IEnumerable<Gasto> GetGastosPorCategoriaEVigencia(int categoriaID, Vigencia vigencia)
        {
            return GetList(g => g.Vigencia.ID == vigencia.ID && g.Categoria.ID == categoriaID).OrderByDescending(g => g.Data).ThenByDescending(g => g.ID);
        }

        public virtual decimal GetGastoTotalPorCategoriaEVigencia(Categoria categoria, Vigencia vigencia)
        {
            return GetList(g => g.Vigencia.ID == vigencia.ID && g.Categoria.ID == categoria.ID)
                .Sum(g => g.Valor); 
        }

        public virtual decimal GetGastoTotalVigenciaSemCategoria(Vigencia vigencia)
        {
            return GetList(g => g.Vigencia.ID == vigencia.ID && g.Categoria == null && g.Categoria.ID == 0)
                .Sum(g => g.Valor); 
        }
    }
}