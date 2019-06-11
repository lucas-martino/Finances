using System;
using System.Collections.Generic;
using System.Linq;
using Finances.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Finances.Domain.Repository.EF
{
    public class GastoRepository : FinancesCRUDRepository<Gasto>, IGastoRepository
    {
        public GastoRepository(FinancesContext dbContext)
            : base(dbContext)
        {
        }

        protected override DbSet<Gasto> DbSet { get { return Context.Gastos; } }

        public IEnumerable<Gasto> GetGastosPorVigencia(Vigencia vigencia)
        {
            return GetList(g => g.Vigencia.Id == vigencia.Id)
                .Include(c => c.Categoria).Include(c => c.Categoria.Pai)
                .OrderByDescending(g => g.Data).ThenByDescending(g => g.Id);
        }

        public decimal GetGastoTotalPorVigencia(Vigencia vigencia)
        {
            return GetList(g => g.Vigencia.Id == vigencia.Id)
                .Sum(g => g.Valor);
        }

        public IEnumerable<Gasto> GetGastosPorCategoriaEVigencia(ulong categoriaID, Vigencia vigencia)
        {
            return GetList(g => g.Vigencia.Id == vigencia.Id && (g.Categoria.Id == categoriaID || g.Categoria.Pai.Id == categoriaID))
                .Include(c => c.Categoria).Include(c => c.Categoria.Pai)
                .OrderByDescending(g => g.Data).ThenByDescending(g => g.Id);
        }

        public decimal GetGastoTotalCompletoPorCategoriaEVigencia(Categoria categoria, Vigencia vigencia)
        {
            return GetList(g => g.Vigencia.Id == vigencia.Id && (g.Categoria.Id == categoria.Id || g.Categoria.Pai.Id == categoria.Id))
                .Sum(g => g.Valor);
        }

        public decimal GetGastoTotalPorCategoriaEVigencia(Categoria categoria, Vigencia vigencia)
        {
            return GetList(g => g.Vigencia.Id == vigencia.Id && g.Categoria.Id == categoria.Id)
                .Sum(g => g.Valor);
        }

        public decimal GetGastoTotalPorCategoria(ulong categoriaID)
        {
            return GetList(g => g.Categoria.Id == categoriaID || g.Categoria.Pai.Id == categoriaID)
                .Sum(g => g.Valor);
        }

        public decimal GetGastoTotalVigenciaSemCategoria(Vigencia vigencia)
        {
            return GetList(g => g.Vigencia.Id == vigencia.Id && g.Categoria == null)
                .Sum(g => g.Valor);
        }

        public IEnumerable<Gasto> GetGastosNaoCategorizadoPorVigencia(Vigencia vigencia)
        {
            return GetList(g => g.Vigencia.Id == vigencia.Id && g.Categoria == null)
                .OrderByDescending(g => g.Data).ThenByDescending(g => g.Id);
        }
    }
}