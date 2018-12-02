using System.Collections.Generic;
using Finances.Domain.Entity;

namespace Finances.Domain.Repository
{
    public interface IGastoRepository : ICRUDRepository<Gasto>
    {
        IEnumerable<Gasto> GetGastosPorVigencia(Vigencia vigencia);
        decimal GetGastoTotalPorVigencia(Vigencia vigencia);
        IEnumerable<Gasto> GetGastosPorCategoriaEVigencia(int categoriaID, Vigencia vigencia);
        decimal GetGastoTotalPorCategoriaEVigencia(Categoria categoria, Vigencia vigencia);
        decimal GetGastoTotalVigenciaSemCategoria(Vigencia vigencia);
    }
}