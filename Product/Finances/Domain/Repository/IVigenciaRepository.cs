using System.Collections.Generic;
using Finances.Domain.Entity;

namespace Finances.Domain.Repository
{
    public interface IVigenciaRepository : IFinancesCRUDRepository<Vigencia>
    {
        IEnumerable<Vigencia> GetVigenciasPorUsuario(Usuario usuario);
        Vigencia GetVigencia(Usuario usuario, int referencia);
        Vigencia GetVigenciaAnterior(Vigencia vigencia);
    }
}