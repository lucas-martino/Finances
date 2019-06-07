using System;
using System.Collections.Generic;
using System.Linq;
using Finances.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Finances.Domain.Repository.EF
{
    public class GastoRepository : CRUDRepository<Gasto, FinancesContext>, IGastoRepository
    {
        public GastoRepository(FinancesContext dbContext) 
            : base(dbContext)
        {
        }

        protected override DbSet<Gasto> DbSet { get { return Context.Gastos; } }

        public IEnumerable<Gasto> GetGastosPorVigencia(Vigencia vigencia)
        {
            return GetList(g => g.Vigencia.ID == vigencia.ID)
                .Include(c => c.Categoria).Include(c => c.Categoria.Pai)
                .OrderByDescending(g => g.Data).ThenByDescending(g => g.ID);
        }

        public decimal GetGastoTotalPorVigencia(Vigencia vigencia)
        {
            return GetList(g => g.Vigencia.ID == vigencia.ID)
                .Sum(g => g.Valor);
        }

        public IEnumerable<Gasto> GetGastosPorCategoriaEVigencia(int categoriaID, Vigencia vigencia)
        {
            return GetList(g => g.Vigencia.ID == vigencia.ID && (g.Categoria.ID == categoriaID || g.Categoria.Pai.ID == categoriaID))
                .Include(c => c.Categoria).Include(c => c.Categoria.Pai)
                .OrderByDescending(g => g.Data).ThenByDescending(g => g.ID);
        }

        public decimal GetGastoTotalCompletoPorCategoriaEVigencia(Categoria categoria, Vigencia vigencia)
        {
            return GetList(g => g.Vigencia.ID == vigencia.ID && (g.Categoria.ID == categoria.ID || g.Categoria.Pai.ID == categoria.ID))
                .Sum(g => g.Valor); 
        }

        public decimal GetGastoTotalPorCategoriaEVigencia(Categoria categoria, Vigencia vigencia)
        {
            return GetList(g => g.Vigencia.ID == vigencia.ID && g.Categoria.ID == categoria.ID)
                .Sum(g => g.Valor); 
        }

        public decimal GetGastoTotalPorCategoria(int categoriaID)
        {
            return GetList(g => g.Categoria.ID == categoriaID || g.Categoria.Pai.ID == categoriaID)
                .Sum(g => g.Valor); 
        }

        public decimal GetGastoTotalVigenciaSemCategoria(Vigencia vigencia)
        {
            return GetList(g => g.Vigencia.ID == vigencia.ID && g.Categoria == null)
                .Sum(g => g.Valor); 
        }

        public IEnumerable<Gasto> GetGastosNaoCategorizadoPorVigencia(Vigencia vigencia)
        {
            return GetList(g => g.Vigencia.ID == vigencia.ID && g.Categoria == null)
                .OrderByDescending(g => g.Data).ThenByDescending(g => g.ID);
        }
    }
}