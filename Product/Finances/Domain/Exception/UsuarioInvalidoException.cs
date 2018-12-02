
namespace Finances.Domain.Exception
{
    public class UsuarioInvalidoException : DomainException
    {
        public UsuarioInvalidoException()
            :base("Usuário inválido, acesso negado.")
        {
            
        }
    }
}