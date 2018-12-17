using System.Collections.Generic;
using Finances.Domain.Entity;

namespace Finances.Domain.Repository
{
    public interface ICategoriaRepository : IFinancesCRUDRepository<Categoria>
    {
        IEnumerable<Categoria> GetCategoriaPorUsuario(Usuario usuario);
        Categoria GetCategoriaPorNomeEUsuario(string nome, Usuario usuario);
        IEnumerable<Categoria> GetCategoriaLevel1PorUsuario(Usuario usuario);
    }
}