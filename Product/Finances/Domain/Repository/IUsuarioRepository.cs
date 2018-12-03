using Finances.Domain.Entity;

namespace Finances.Domain.Repository
{
    public interface IUsuarioRepository : IFinancesReadRepository<Usuario>
    {
        Usuario GetUsuarioByLoginSenha(string login, string senha);
    }
}