using System.Collections.Generic;
using Finances.Domain.Entity;

namespace Finances.Domain.Repository
{
    public interface IGastoRepository : IFinancesCRUDRepository<Gasto>
    {
        IEnumerable<Gasto> GetGastosPorVigencia(Vigencia vigencia);
        decimal GetGastoTotalPorVigencia(Vigencia vigencia);
        IEnumerable<Gasto> GetGastosPorCategoriaEVigencia(ulong categoriaID, Vigencia vigencia);
        decimal GetGastoTotalPorCategoriaEVigencia(Categoria categoria, Vigencia vigencia);
        decimal GetGastoTotalPorCategoria(ulong categoriaID);
        decimal GetGastoTotalVigenciaSemCategoria(Vigencia vigencia);
        IEnumerable<Gasto> GetGastosNaoCategorizadoPorVigencia(Vigencia vigencia);
        decimal GetGastoTotalCompletoPorCategoriaEVigencia(Categoria categoria, Vigencia vigencia);
    }
}