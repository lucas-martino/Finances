using Framework.Domain.Exception;

namespace Finances.Domain.Exception
{
    public class UsuarioInvalidoException : FinancesDomainException
    {
        public UsuarioInvalidoException()
            :base("Usuário inválido, acesso negado.")
        {
            
        }
    }
}