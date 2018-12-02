using Finances.Domain.Entity;

namespace Finances.Domain.Repository
{
    public interface IUsuarioRepository : IReadRepository<Usuario>
    {
        Usuario GetUsuarioByLoginSenha(string login, string senha);
    }
}