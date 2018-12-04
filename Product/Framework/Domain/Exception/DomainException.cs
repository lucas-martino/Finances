
namespace Framework.Domain.Exception
{
    public abstract class DomainException : System.Exception
    {
        public DomainException()
            :base()
        {

        }
        public DomainException(string mensagem)
            :base(mensagem)
        {

        }
        public DomainException(string mensagem, System.Exception innerException)
            :base(mensagem, innerException)
        {

        }
    }
}