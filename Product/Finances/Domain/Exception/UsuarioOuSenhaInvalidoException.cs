using Framework.Domain.Exception;

namespace Finances.Domain.Exception
{
    public class UsuarioOuSenhaInvalidoException : FinancesDomainException
    {
        public UsuarioOuSenhaInvalidoException()
            :base("Usuário ou Senha inválido.")
        {
            
        }
    }
}