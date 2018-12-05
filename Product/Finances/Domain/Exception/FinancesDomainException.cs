
using Framework.Domain.Exception;

namespace Finances.Domain.Exception
{
    public abstract class FinancesDomainException : DomainException
    {
        public FinancesDomainException()
            :base()
        {

        }
        public FinancesDomainException(string mensagem)
            :base(mensagem)
        {

        }
        public FinancesDomainException(string mensagem, System.Exception innerException)
            :base(mensagem, innerException)
        {

        }
    }
}