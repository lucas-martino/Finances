
namespace Finances.Domain.Exception
{
    public class UsuarioOuSenhaInvalidoException : DomainException
    {
        public UsuarioOuSenhaInvalidoException()
            :base("Usuário ou Senha inválido.")
        {
            
        }
    }
}