using System.Collections.Generic;
using Finances.Domain.Entity;

namespace Finances.Domain.Repository
{
    public interface ICategoriaRepository : ICRUDRepository<Categoria>
    {
        IEnumerable<Categoria> GetCategoriaPorUsuario(Usuario usuario);
    }
}